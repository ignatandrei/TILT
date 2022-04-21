var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddSwaggerGen();
bool IsInCI = new XAboutMySoftware_78102118871091131225395110108769286().IsInCI;

builder.Services.AddDbContextFactory<ApplicationDBContext>(
    options =>
    {
        if (IsInCI)
        {
            var cn = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(cn);
        }

        else
        {
            var cn = "Data Source=Tilt.db";
            options.UseSqlite(cn);
        }
    }
     )
   ;
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseBlocklyUI(app.Environment);
app.UseAuthorization();

app.MapControllers();
app.UseAMS();
app.UseBlocklyAutomation();
using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    dbcontext.Database.EnsureCreated();    
}

app.Run();

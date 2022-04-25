var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddSwaggerGen();
bool IsBuildFromCI = new XAboutMySoftware_78102118871091131225395110108769286().IsInCI;
var cnSqlite = "Data Source=Tilt.db";

var hc = builder.Services.AddHealthChecks();
if (IsBuildFromCI)
{
    hc.AddSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"],name:"database SqlServer");
}
else
{
    hc.AddSqlite(cnSqlite, name: "database Sqlite");
}

var key = builder.Configuration["MySettings:secretToken"];
builder.Services.AddAuthentication(x =>
{

    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
builder.Services.AddScoped<IAuthUrl, AuthUrl>();
builder.Services.AddScoped<ISearchDataTILT_URL, SearchDataTILT_URL>();

builder.Services
     .AddHealthChecksUI(setup =>
     {

         var health = "/healthz";
         if (IsBuildFromCI)
         {
             health = builder.Configuration["MySettings:url"] + health;
         }
         setup.AddHealthCheckEndpoint("me",health );
         setup.SetEvaluationTimeInSeconds (60*60);
         //setup.SetHeaderText
         setup.MaximumHistoryEntriesPerEndpoint(10);
     }
        
    )
     .AddInMemoryStorage()
     ;


builder.Services.AddDbContextFactory<ApplicationDBContext>(
    options =>
    {
        if (IsBuildFromCI)
        {
            var cn = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(cn);
        }

        else
        {
            options.UseSqlite(cnSqlite);
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
app.UseAuthentication();
app.MapControllers();
app.UseAMS();
app.UseBlocklyAutomation();
if (!IsBuildFromCI)
{
    using (var scope = app.Services.CreateScope())
    {
        if (File.Exists("Tilt.db"))
            File.Delete("Tilt.db");

        var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        dbcontext.Database.EnsureCreated();

        //seed db
        dbcontext.TILT_URL.Add(new TILT_URL()
        {
            Secret = "Andrei",
            URLPart = "ignatandrei"
        });
        await dbcontext.SaveChangesAsync();
    }
}

app.MapHealthChecks("healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI(setup =>
{

});
app.Run();

using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

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
builder.Services.AddAuthorization(options=>

    options.AddPolicy("CustomBearer", policy =>
    {
        policy.AuthenticationSchemes.Add("CustomBearer");
        policy.RequireAuthenticatedUser();
    }));
builder.Services.AddAuthentication()
           .AddJwtBearer("CustomBearer",options =>
           {
              
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
               options.Events = new JwtBearerEvents()
               {
                   OnMessageReceived = ctx =>
                   {
                       if (!(ctx?.Request?.Headers?.ContainsKey("Authorization") ?? true))
                       {
                           ctx.NoResult();
                           return Task.CompletedTask;
                       };
                       var auth = ctx.Request.Headers["Authorization"].ToString();
                       if (string.IsNullOrEmpty(auth))
                       {
                           ctx.NoResult();
                           return Task.CompletedTask;
                       }
                       if (!auth.StartsWith("CustomBearer ", StringComparison.OrdinalIgnoreCase))
                       {
                           ctx.NoResult();
                           return Task.CompletedTask;
                       }

                       ctx.Token = auth.Substring("CustomBearer ".Length).Trim();
                       return Task.CompletedTask;

                   }
               };
               //x.Events.OnTokenValidated=
           });
builder.Services.AddScoped<IAuthUrl, AuthUrl>();
builder.Services.AddScoped<I_InsertDataApplicationDBContext, InsertDataApplicationDBContext>();
builder.Services.AddScoped<ISearchDataTILT_URL, SearchDataTILT_URL>();
builder.Services.AddScoped<ISearchDataTILT_Note, SearchDataTILT_Note>();
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


builder.Services.AddOpenTelemetryTracing(b =>
{
    // uses the default Jaeger settings
    //b.AddJaegerExporter();

    // receive traces from our own custom sources
    b.AddSource("TILT_SOURCE");

    // decorate our service name so we can find it when we look inside Jaeger
    b.SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService("TILTWebAPI", "TILT"));

    // receive traces from built-in sources
    b.AddHttpClientInstrumentation();
    b.AddAspNetCoreInstrumentation();
    b.AddSqlClientInstrumentation();
    b.AddEntityFrameworkCoreInstrumentation();
});

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
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(it => true);
                      });
});
var app = builder.Build();

app.UseStaticFiles();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseBlocklyUI(app.Environment);
app.UseCors("AllowAll");
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
        dbcontext.TILT_URL.Add(new TILT_URL()
        {
            Secret = "test",
            URLPart = "test"
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

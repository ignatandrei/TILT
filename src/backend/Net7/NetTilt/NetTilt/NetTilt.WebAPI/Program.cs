
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
builder.Services.AddServerTiming();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails(options =>
{
    options.IncludeExceptionDetails = (ctx, ex) => true;
});

bool IsBuildFromCI = new XAboutMySoftware_78102118871091131225395110108769286().IsInCI;
var cnSqlite = "Data Source=Tilt.db";

var hc = builder.Services.AddHealthChecks();
//if (IsBuildFromCI)
//{
//    hc.AddSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"],name:"database SqlServer");
//}
//else
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
builder.Services.AddScoped<IMyTilts, MyTilts>();
builder.Services.AddScoped<I_InsertDataApplicationDBContext, InsertDataApplicationDBContext>();
builder.Services.AddScoped<ISearchDataTILT_URL, SearchDataTILT_URL>();
builder.Services.AddScoped<ISearchDataTILT_Note, SearchDataTILT_Note>();
builder.Services.AddScoped<IPublicTILTS, PublicTILTS>();
builder.Services
     .AddHealthChecksUI(setup =>
     {

         var health = "/healthz";
         if (IsBuildFromCI)
         {
             health = builder.Configuration["MySettings:url"] + health;
         }
         setup.AddHealthCheckEndpoint("me",health );
         setup.SetEvaluationTimeInSeconds (60);
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
    b.AddAzureMonitorTraceExporter(o =>
     {
         o.ConnectionString = "InstrumentationKey=4772445f-40dd-44ae-b7d5-2c2ea33b9de3;IngestionEndpoint=https://westus2-2.in.applicationinsights.azure.com/;LiveEndpoint=https://westus2.livediagnostics.monitor.azure.com/";
     });
    //b.AddConsoleExporter(c =>
    //{
    //});
    // receive traces from our own custom sources
    b.AddSource("TILT_SOURCE*");

    // decorate our service name so we can find it when we look inside Jaeger
    b.SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService("TILTWebAPI", "TILT")        
        );

    // receive traces from built-in sources
    b.AddHttpClientInstrumentation(c =>
    {
        c.RecordException = true;
        c.SetHttpFlavor = true;
    });
    b.AddAspNetCoreInstrumentation(c =>
    {
        c.RecordException = true;
        c.EnableGrpcAspNetCoreSupport = true;
        
    });
    b.AddSqlClientInstrumentation(c =>
    {
        c.EnableConnectionLevelAttributes = true;
        c.RecordException = true;
        c.SetDbStatementForStoredProcedure = true;
        c.SetDbStatementForText = true;
       
    });
    b.AddEntityFrameworkCoreInstrumentation(c =>
    {
        c.SetDbStatementForStoredProcedure = true;
        c.SetDbStatementForText = true;

    });
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
builder.Services.AddMemoryCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(it => true);
                      });
});
builder.Services.AddScoped<ServerTiming>();


var noLimit = RateLimitPartition.GetNoLimiter("");

Func<string, RateLimitPartition<string>> simpleLimiter =
    (string address) =>
RateLimitPartition.GetFixedWindowLimiter(address, _ =>
{
    return new FixedWindowRateLimiterOptions()
    {
        PermitLimit = 3,
        Window = TimeSpan.FromMinutes(1),
        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
        QueueLimit = 1
    };

});


builder.Services.AddRateLimiter(opt =>
{
    opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    opt.OnRejected = (ctx, ct) =>
    {
        ctx.HttpContext.Response.Headers.Add("tiltLimit", "please try later");
        return ValueTask.CompletedTask;
    };
    opt.AddPolicy("UnlimitMeAndLocalHost", context =>
    {
        
        var host = context.Request.Host;
        var hostName = host.HasValue ? host.Host : "";
        if (string.IsNullOrWhiteSpace(hostName))
        {
            Console.WriteLine("no host???");
            return simpleLimiter("");
        }
        if (string.Equals(hostName,"localhost",StringComparison.InvariantCultureIgnoreCase))
        {
            Console.WriteLine("localhost have no limit");
            return noLimit;
        }
        //chrome does not send origin, if same site
        var hasOrigin = context.Request.Headers.TryGetValue("Origin", out var origin);
        //maybe also verify referer?
        if (!hasOrigin)
        {
            Console.WriteLine("no origin -same site?");
            return noLimit;

        }
        //edge sends origin
        var originHost = origin.ToString();
        //removing scheme
        if (originHost.StartsWith("http://"))
        {
            originHost = originHost.Substring(7);
        }
        if (originHost.StartsWith("https://"))
        {
            originHost = originHost.Substring(8);
        }
        var fullHost = context.Request.Host.Host;
        Console.WriteLine($"has origin {originHost} , full host {fullHost}");
        if (string.Equals(fullHost,originHost, StringComparison.CurrentCultureIgnoreCase))
        {
            Console.WriteLine($"same site - no cors");
            return noLimit;
        }
        //return noLimit;
        return simpleLimiter(origin.ToString());
    });
});


var app = builder.Build();
app.UseProblemDetails();
app.UseStatusCodePages();
app.UseRateLimiter();
app.UseServerTiming();
app.UseMiddleware<ServerTiming>();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();

app.UseBlocklyUI(app.Environment);
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers().RequireRateLimiting("UnlimitMeAndLocalHost");
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
            URLPart = "TestAddWithNoData"
        });
        dbcontext.TILT_URL.Add(new TILT_URL()
        {
            Secret = "test",
            URLPart = "ClickToSeeMyTILTS"
        });
        await dbcontext.SaveChangesAsync();

        dbcontext.TILT_Note.Add(new TILT_Note()
        {
            ForDate = DateTime.UtcNow.AddDays(-9),
            IDURL = 2,
            Text = "I learned how to make an web app with .NET core on backend",
            TimeZoneString = "Europe/Bucharest"
        });
        dbcontext.TILT_Note.Add(new TILT_Note()
        {
            ForDate = DateTime.UtcNow.AddDays(-8),
            IDURL = 2,
            Text = "I learned how to make an web app with Angular on frontend",
            TimeZoneString = "Europe/Bucharest"
        });
        dbcontext.TILT_Note.Add(new TILT_Note()
        {
            ForDate = DateTime.UtcNow.AddDays(-7),
            IDURL = 2,
            Text = "and learned how to deploy bundled together",
            TimeZoneString = "Europe/Bucharest"
        });
        dbcontext.TILT_Note.Add(new TILT_Note()
        {
            ForDate = DateTime.UtcNow.AddDays(-6),
            IDURL = 2,
            Text = "so I made this app - Things I Learned Today - TILT",
            TimeZoneString = "Europe/Bucharest"
        });
        dbcontext.TILT_Note.Add(new TILT_Note()
        {
            ForDate = DateTime.UtcNow.AddDays(-6),
            IDURL = 2,
            Text = "see link",
            Link = "https://tiltwebapp.azurewebsites.net/",
            TimeZoneString = "Europe/Bucharest"
        });
        dbcontext.TILT_Note.Add(new TILT_Note()
        {
            ForDate = DateTime.UtcNow.AddDays(-3),
            IDURL = 2,
            Text = "it has also swagger",
            Link = "https://tiltwebapp.azurewebsites.net/swagger",
            TimeZoneString = "Europe/Bucharest"
        });
        dbcontext.TILT_Note.Add(new TILT_Note()
        {
            ForDate = DateTime.UtcNow.AddDays(-2),
            IDURL = 2,
            Text = "it has also BlocklyAutomation",
            Link = "https://tiltwebapp.azurewebsites.net/BlocklyAutomation",
            TimeZoneString = "Europe/Bucharest"
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

using var source = new ActivitySource("TILT_SOURCE");
using (var activity = source.StartActivity("StartApp"))
{

    activity?.SetTag("On", DateTime.UtcNow);
    activity?.SetStatus(Status.Ok);

}

app.MapGet("/EXAMPLE/{orderId}", Results<BadRequest, Ok<string>> (int orderId)
    => orderId > 999 ? TypedResults.BadRequest() : TypedResults.Ok("done"));


app.MapUsefullAll();
app.MapFallbackToFile("AngTilt/{*path:nonfile}", "/AngTilt/index.html");
app.MapFallbackToFile("ReactTilt/{*path:nonfile}", "/ReactTilt/index.html");
app.Run();

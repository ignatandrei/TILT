using Microsoft.Extensions.DependencyInjection;
using NetTilt.Logic;

namespace NetTilt.Tests;
public  abstract partial class RealDBTests: FeatureFixture
{
    public abstract void StartDatabase();

    public abstract void StopDatabase();

    public abstract IServiceCollection AddDB( IServiceCollection sc);


    public void ConstructServiceProvider()
    {
        string guid = Guid.NewGuid().ToString("N");

        var inMemorySettings = new Dictionary<string, string> {
{"TopLevelKey", "TopLevelValue"},
{"MySettings:secretToken", "AnotherKeySettingsForGeneratingJwtForAuthenticationAndAuthorization"},
//...populate as needed for the test
};

        IConfiguration configuration = new ConfigurationBuilder()
.AddInMemoryCollection(inMemorySettings)
.Build();

        var st = new Mock<IServerTiming>();

        serviceProvider = AddDB(new ServiceCollection())
.AddScoped<IServerTiming>(sp => st.Object)
.AddLogging()
.AddMemoryCache()
.AddSingleton<IConfiguration>(configuration)
.AddScoped<IMyTilts, MyTilts>()
.AddScoped<IAuthUrl, AuthUrl>()
.AddScoped<I_InsertDataApplicationDBContext, InsertDataApplicationDBContext>()
.AddScoped<ISearchDataTILT_URL, SearchDataTILT_URL>()
.AddScoped<ISearchDataTILT_Note, SearchDataTILT_Note>()
.AddScoped<IPublicTILTS, PublicTILTS>()
.BuildServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {

            var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            dbcontext.Database.EnsureCreated();

        }
    }
    [SetUp]
    public void Start()
    {
        StartDatabase();
    }
    [TearDown]
    public void Stop()
    {
        StopDatabase();
    }
}

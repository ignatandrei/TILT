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


    [Scenario]
    public async Task NoUsers() //scenario name
    {
        await Runner

             .AddAsyncSteps(
            _ => Then_No_User_IsRegistered(),
             _ => Then_No_Login()
             )
             .RunAsync();
    }
    [Scenario]
    public async Task LoginUsers() //scenario name
    {
        var users = Table.For(

            new TILT_URL()
            {
                URLPart = "test",
                Secret = "secretTest"
            }
        );

        await Runner
             .AddAsyncSteps(
            _ => When_Those_Users_URL_To_Be_Registerer(users),
             _ => Then_The_Users_Could_Login(users),
             _ => And_The_JWT_Can_Be_Decrypted(users),
             _ => And_The_JWT_Can_Be_Decrypted_And_Have_Ids(users),
              _ => And_Exists_Public_Tilts(users),
              _ => And_No_Tilts_Yet(users)

             )
             .RunAsync();
    }

    [Scenario]
    public async Task CreateTilts()
    {
        var users = Table.For(

            new TILT_URL()
            {
                URLPart = "test",
                Secret = "secretTest"
            }
        );

        await Runner
             .AddAsyncSteps(
            _ => When_Those_Users_URL_To_Be_Registerer(users),
             _ => Then_The_Users_Have_No_Tilt_Today(users),
             _ => When_The_Users_Make_A_Tilt(users),
             _ => Then_The_Users_Have_Tilt_Today(users),
             _ => And_Have_Tilts(users)
             )
             .RunAsync();
    }
    [Scenario]
    public async Task VerifyPublicTiltsHasTheNewTilt()
    {
        var users = Table.For(

            new TILT_URL()
            {
                URLPart = "test",
                Secret = "secretTest"
            }
        );

        await Runner
             .AddAsyncSteps(
            _ => When_Those_Users_URL_To_Be_Registerer(users),
             _ => Then_Public_Tilts_Have_No_Items(users),
             _ => Then_Public_Tilts_Have_No_Items(users),//verify caching
             _ => When_The_Users_Make_A_Tilt(users),
             _ => And_Have_Tilts(users)
             )
             .RunAsync();
    }
}

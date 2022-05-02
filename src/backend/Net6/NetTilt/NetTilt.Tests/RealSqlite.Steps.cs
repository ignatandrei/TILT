
namespace NetTilt.Tests;

public partial class RealSqlite : FeatureFixture
{
    string guid=Guid.NewGuid().ToString("x");

    ServiceProvider? serviceProvider;

    public void Given_Empty_Database_Setup()
    {
        var inMemorySettings = new Dictionary<string, string> {
{"TopLevelKey", "TopLevelValue"},
{"MySettings:secretToken", "AnotherKeySettingsForGeneratingJwtForAuthenticationAndAuthorization"},
//...populate as needed for the test
};

        IConfiguration configuration = new ConfigurationBuilder()
.AddInMemoryCollection(inMemorySettings)
.Build();
        serviceProvider = new ServiceCollection()
.AddLogging()
.AddDbContextFactory<ApplicationDBContext>(
    options =>
    {
        options.UseSqlite("Data Source={g}.db");
    }
     )
.AddSingleton<IConfiguration>(configuration)
.AddScoped<IMyTilts, MyTilts>()
.AddScoped<IAuthUrl, AuthUrl>()
.AddScoped<IMyTilts, MyTilts>()
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

    public async Task Then_No_User_IsRegistered()
    {
        var tiltsUrl = serviceProvider.GetRequiredService<IPublicTILTS>();
        var data = await tiltsUrl.PublicTiltsURL().ToArrayAsync();
        Assert.NotNull(data);
        Assert.IsEmpty(data);
    }
    public async Task Then_No_Login()
    {
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        var data = await auth.Login("asdad", "asdasd");
        Assert.Null(data);
    }
}

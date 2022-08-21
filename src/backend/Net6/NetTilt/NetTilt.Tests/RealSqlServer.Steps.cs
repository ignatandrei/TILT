namespace NetTilt.Tests;
public partial class RealSqlServer:FeatureFixture
{
    ServiceProvider? serviceProvider;
    IContainerService? container;
    public void StopDatabase()
    {
        if (container == null)
            return;
        container.Dispose();
        container = null;
    }
    public void StartDatabase()
    {
        container =
    new Builder()
    .UseContainer()
    .WithName("sql")
    .UseImage("mcr.microsoft.com/mssql/server:2022-latest")
    .ExposePort(1433, 1433)
    .WithEnvironment("SA_PASSWORD=<YourStrong@Passw0rd>", "ACCEPT_EULA=Y")
    .WaitForMessageInLog("Starting up database 'tempdb'.", TimeSpan.FromSeconds(30))
    .Build()
    .Start();
    }
    public void Given_Empty_Database_Setup()
    {
        string guid = Guid.NewGuid().ToString("x");

        var inMemorySettings = new Dictionary<string, string> {
{"TopLevelKey", "TopLevelValue"},
{"MySettings:secretToken", "AnotherKeySettingsForGeneratingJwtForAuthenticationAndAuthorization"},
//...populate as needed for the test
};

        IConfiguration configuration = new ConfigurationBuilder()
.AddInMemoryCollection(inMemorySettings)
.Build();
        var st = new Mock<IServerTiming>();

        serviceProvider = new ServiceCollection()
.AddScoped<IServerTiming>(sp => st.Object)
.AddLogging()
.AddDbContextFactory<ApplicationDBContext>(
    options =>
    {
        options.UseSqlServer("Data Source=.;Initial Catalog=tilt;UId=sa;pwd=<YourStrong@Passw0rd>");
    }
     )
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

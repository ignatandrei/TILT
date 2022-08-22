using NetTilt.Logic;
using System.Threading;

namespace NetTilt.Tests;
public partial class RealSqlServer:RealDBTests
{
    
    IContainerService? container;


    public override void StopDatabase()
    {
        if (container == null)
            return;
        container.Dispose();
        container = null;
    }
    public override void StartDatabase()
    {
        //string guid = Guid.NewGuid().ToString("N");
        string uniqueId = Interlocked.Increment(ref uniq).ToString(); //Guid.NewGuid().ToString("N");
        container =
    new Builder()
    .UseContainer()
    .WithName("sql"+ uniqueId)
    .UseImage("mcr.microsoft.com/mssql/server:2022-latest")
    .ExposePort(1433, 1433)
    .WithEnvironment("SA_PASSWORD=<YourStrong@Passw0rd>", "ACCEPT_EULA=Y")
    .WaitForMessageInLog("Starting up database 'tempdb'.", TimeSpan.FromSeconds(30))
    .Build()
    .Start();
        Given_Empty_Database_Setup();

    }
    static int uniq = 0;
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
        var st = new Mock<IServerTiming>();

        base.serviceProvider = new ServiceCollection()
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
        using (var scope = base.serviceProvider.CreateScope())
        {

            var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            dbcontext.Database.EnsureCreated();

        }
    }
    
}

using NetTilt.Logic;
using System.Threading;

namespace NetTilt.Tests;
public partial class RealSqlServer : RealDBTests
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
    .WithName("sql" + uniqueId)
    .UseImage("mcr.microsoft.com/mssql/server:2022-latest")
    .ExposePort(1433, 1433)
    .WithEnvironment("SA_PASSWORD=<YourStrong@Passw0rd>", "ACCEPT_EULA=Y")
    .WaitForMessageInLog("Starting up database 'tempdb'.", TimeSpan.FromSeconds(30))
    .Build()
    .Start();
        ConstructServiceProvider();

    }
    static int uniq = 0;
    public override IServiceCollection AddDB(IServiceCollection sc)
    {
        return sc.AddDbContextFactory<ApplicationDBContext>(
    options =>
    {
        options.UseSqlServer("Data Source=.;Initial Catalog=tilt;UId=sa;pwd=<YourStrong@Passw0rd>");
    }
     );

    }
}

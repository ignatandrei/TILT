using NetTilt.Logic;
using System.Threading;

namespace NetTilt.Tests;
public partial class RealSqlServer : RealDBTests
{

    
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

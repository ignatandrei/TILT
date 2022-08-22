
namespace NetTilt.Tests;

public partial class RealSqlite : RealDBTests
{
    public override IServiceCollection AddDB(IServiceCollection sc)
    {
        string guid = Guid.NewGuid().ToString("N");
        return sc.AddDbContextFactory<ApplicationDBContext>(
        options =>
        {
            options.UseSqlite($"Data Source={guid}.db");
        }
         )

    ;
    }

}

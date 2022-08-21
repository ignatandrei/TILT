using System.Text;

namespace NetTilt.Tests;
[FeatureDescription(@"All tests")]
[Label("Sqlite")]
public partial class RealSqlServer
{
    [Scenario]
    public async Task NoUsers() //scenario name
    {
        await Runner
            .AddSteps(_ => StartDatabase())

            .AddSteps(_ => Given_Empty_Database_Setup())
             .AddAsyncSteps(
            _ => Then_No_User_IsRegistered(),
             _ => Then_No_Login()
             )
             .AddSteps(_ => StopDatabase())
             .RunAsync();
    }
}

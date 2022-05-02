namespace NetTilt.Tests;
[FeatureDescription(@"All tests")]
[Label("Sqlite")]

public partial class RealSqlite
{

    [Scenario]
    public async Task NewTilt() //scenario name
    {
        await Runner
             .AddSteps(_ => Given_Empty_Database_Setup())
             .AddAsyncSteps(
            _ => Then_No_User_IsRegistered(),                          
             _=> Then_No_Login()
             )
             .RunAsync();
    }
}

namespace NetTilt.Tests;
[FeatureDescription(@"All tests")]
[Label("Sqlite")]

public partial class RealSqlite
{

    [Scenario]
    public async Task NoUsers() //scenario name
    {
        await Runner
             .AddSteps(_ => Given_Empty_Database_Setup())
             .AddAsyncSteps(
            _ => Then_No_User_IsRegistered(),                          
             _=> Then_No_Login()
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
             .AddSteps(_ => Given_Empty_Database_Setup())
             .AddAsyncSteps(
            _ => Given_Those_Users_URL_To_Be_Registerer(users),
             _ => Then_The_Users_Could_Login(users),
             _=> Then_The_JWT_Can_Be_Decrypted(users),
             _=> Then_The_JWT_Can_Be_Decrypted_And_Have_Ids(users),
              _ => Then_Exists_Public_Tilts(users)

             )
             .RunAsync();
    }
}

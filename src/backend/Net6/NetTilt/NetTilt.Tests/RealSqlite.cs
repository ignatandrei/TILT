namespace NetTilt.Tests;
[FeatureDescription(@"All tests")]
[Label("Sqlite")]

public partial class RealSqlite
{
    public override void StartDatabase()
    {
        ConstructServiceProvider();
    }
    
    public override void StopDatabase()
    {
        
    }

    [Scenario]
    public async Task NoUsers() //scenario name
    {
        await Runner
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
             _=> When_The_Users_Make_A_Tilt(users),
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

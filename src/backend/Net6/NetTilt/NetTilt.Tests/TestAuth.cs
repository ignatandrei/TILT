namespace NetTilt.Tests;

public partial class TestAuth
{
    [Scenario]
    public async Task NewTilt() //scenario name
    {
        await Runner
             .AddSteps(_ => Given_Correct_Name_And_Password())
             .AddAsyncSteps(
                _=> Then_Can_Obtain_The_JWT()
             )
             .AddSteps(
                _ => Then_Can_Decrypt_The_JWT(),
                _=> Then_Can_Verify_The_URL_ID()

             )
             .RunAsync();
    }
}

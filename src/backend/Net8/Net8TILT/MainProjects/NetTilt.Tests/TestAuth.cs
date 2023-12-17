namespace NetTilt.Tests;
[FeatureDescription(@"Test aithentication")]
[Label("FakesMocks")]
public partial class TestAuth
{
    [Scenario]
    public async Task CorrectNameAndPassword() //scenario name
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
    [Scenario]
    public async Task WrongNamePassword() //scenario name
    {
        await Runner
             .AddSteps(_ => Given_Wrong_Name())
             .AddAsyncSteps(
                _ => Then_Can_NOT_Obtain_The_JWT()
             )
             .RunAsync();
    }
}


namespace NetTilt.Tests;

[FeatureDescription(@"Test add a new TILT")]
[Label("FakesMocks")]
public partial class TestAddNewTilt
{
    
    [SetUp]
    public void Setup()
    {

    }
    [Scenario]
    public async Task NewTilt() //scenario name
    {
       await Runner
            .AddSteps(_ => Given_No_TILT_For_URL())
            .AddAsyncSteps(_ => Then_Can_Add_A_New_TILT())
            .RunAsync();
    }
    [Scenario]
    public async Task Existing_TILT_Start_Of_The_Day() 
    {
        var minutes = DateTime.UtcNow.Subtract(DateTime.UtcNow.Date).TotalMinutes;
        minutes--;
        await Runner
              .AddSteps(_ => Given_Today_Is(DateTime.UtcNow))
             .AddSteps(_ => Given_Exists_One_TILT_ForDate(DateTime.UtcNow.AddMinutes(-minutes)))
             .AddAsyncSteps(_ => Then_Can_NOT_Add_A_New_TILT())
             .RunAsync();
    }
    [Scenario]
    [TestCase(2)]
    [TestCase(20)]
    [TestCase(1)]
    public async Task ExistingTILT_DaysAgo(int days) //scenario name
    {
        await Runner
              .AddSteps(_ => Given_Today_Is(DateTime.UtcNow))
             .AddSteps(_ => Given_Exists_One_TILT_ForDate(DateTime.UtcNow.AddDays(-days)))
             .AddAsyncSteps(_ => Then_Can_Add_A_New_TILT())
             .RunAsync();
    }
    [Scenario]
    [TestCase(1,false)]
    [TestCase(2, false)]
    [TestCase(20, false)]
    [TestCase(100, false)]
    [TestCase(200, false)]
    [TestCase(4*60, false)]
    [TestCase(5 * 60, false)]
    [TestCase(6 * 60, false)]
    [TestCase(7 * 60, false)]
    [TestCase(8 * 60, false)]
    [TestCase(9 * 60, false)]
    [TestCase(10 * 60, false)]
    [TestCase(11* 60, false)]
    [TestCase(12 * 60, false)]
    [TestCase(23 * 60, false)]
    [TestCase(23 * 60+58, false)]
    [TestCase(24 * 60 + 1, true)]
    public async Task ExistingTILT_MinutesAgo(int minutes, bool canAdd) //scenario name
    {
        var run =  Runner
              .AddSteps(_ => Given_Today_Is(DateTime.UtcNow))
              .AddSteps(_ => Given_Exists_One_TILT_ForDate(DateTime.Now.Date.AddDays(1).AddMinutes(-minutes)));

        if (canAdd)
            run = run
             .AddAsyncSteps(_ => Then_Can_Add_A_New_TILT());
        else
            run = run
             .AddAsyncSteps(_ => Then_Can_NOT_Add_A_New_TILT());

        await run.RunAsync();
    }
    [Test]
    public async Task TestAddOneTilt()
    {
        SetupTilt(null);
        var myTilt= serviceProvider.GetRequiredService<IMyTilts>();
        var data= await myTilt.AddTILT(new TILT_Note_Table(), null);
        Assert.IsNotNull(data);
    }
}

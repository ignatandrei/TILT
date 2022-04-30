
namespace NetTilt.Tests;

[FeatureDescription(@"Test add a new TILT")]
public partial class TestAdd
{
    ServiceProvider? serviceProvider;
    
    [SetUp]
    public void Setup()
    {

    }
    [Scenario]
    [Label("FAKES")]
    public async Task NewTilt() //scenario name
    {
       await Runner
            .AddSteps(_ => Given_No_TILT_For_URL())
            .AddAsyncSteps(_ => Then_Can_Add_A_New_TILT(true))
            .RunAsync();
    }
    [Scenario]
    [Label("FAKES")]
    [TestCase(2)]
    [TestCase(20)]
    [TestCase(1)]
    public async Task ExistingTILT_DaysAgo(int days) //scenario name
    {
        await Runner
             .AddSteps(_ => Given_One_TILT_ForDate(DateTime.UtcNow.AddDays(-days)))
             .AddAsyncSteps(_ => Then_Can_Add_A_New_TILT(true))
             .RunAsync();
    }
    [Scenario]
    [Label("FAKES")]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(20)]
    [TestCase(100)]
    [TestCase(200)]
    [TestCase(4*60)]
    [TestCase(5 * 60)]
    [TestCase(6 * 60)]
    [TestCase(7 * 60)]
    [TestCase(8 * 60)]
    [TestCase(9 * 60)]
    [TestCase(10 * 60)]
    [TestCase(11* 60)]
    [TestCase(12 * 60)]
    [TestCase(23 * 60)]
    [TestCase(23 * 60+58)]
    public async Task ExistingTILT_MinutesAgo(int minutes) //scenario name
    {
        await Runner
             .AddSteps(_ => Given_One_TILT_ForDate(DateTime.UtcNow.Date.AddDays(1).AddMinutes(-minutes)))
             .AddAsyncSteps(_ => Then_Can_Add_A_New_TILT(false))
             .RunAsync();
    }
    [Test]
    public async Task TestAddOneTilt()
    {
        var myTilt= serviceProvider.GetRequiredService<IMyTilts>();
        var data= await myTilt.AddTILT(new TILT_Note_Table(), null);
        Assert.IsNotNull(data);
    }
}


namespace NetTilt.Tests;

partial  class TestAdd: FeatureFixture
{
    async Task Then_Can_Add_A_New_TILT(bool result)
    {
        var myTilt = serviceProvider.GetRequiredService<IMyTilts>();
        var data = await myTilt.AddTILT(new TILT_Note_Table(), null);
        if(result)
            Assert.IsNotNull(data);
        else
            Assert.IsNull(data);
    }
    void SetupTilt(DateTime? oneTITL)
    {
        var searchUrl = new Mock<ISearchDataTILT_URL>();
        var insert = new Mock<I_InsertDataApplicationDBContext>();
        insert.Setup(x => x.InsertTILT_Note(It.IsAny<TILT_Note_Table>())).Returns(Task.FromResult(new TILT_Note_Table()));




        var searchNote = new Mock<ISearchDataTILT_Note>();
        TILT_Note[] retArray;
        if (oneTITL.HasValue)
        {
            retArray = new TILT_Note[1];
            retArray[0] = new TILT_Note();
            retArray[0].ForDate= oneTITL.Value;
        }
        else
        {
            retArray = new TILT_Note[0];
        }
        
        searchNote.Setup(it => it.TILT_NoteFind_AsyncEnumerable(It.IsAny<SearchTILT_Note?>())).Returns(retArray.ToAsyncEnumerable());

        var auth = new Mock<IAuthUrl>();
        auth.Setup(x => x.MainUrlId(null)).Returns(1);
        //var q = new SearchDataTILT_URL(null);

        serviceProvider = new ServiceCollection()
        .AddLogging()
        .AddScoped<IMyTilts, MyTilts>()
        .AddScoped<ISearchDataTILT_URL>(sp => searchUrl.Object)
        .AddScoped<I_InsertDataApplicationDBContext>(sp => insert.Object)
        .AddScoped<ISearchDataTILT_Note>(sp => searchNote.Object)
        .AddScoped<IAuthUrl>(sp => auth.Object)
        //.AddSingleton<IBarService, BarService>()
        .BuildServiceProvider();

    }
    void Given_No_TILT_For_URL()
    {
        SetupTilt(null);        
    }
    void Given_One_TILT_ForDate(DateTime date)
    {
        SetupTilt(date);
    }
}

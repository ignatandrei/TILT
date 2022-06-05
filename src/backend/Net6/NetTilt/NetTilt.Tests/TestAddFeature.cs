
namespace NetTilt.Tests;

partial  class TestAddNewTilt: FeatureFixture
{
    ServiceProvider? serviceProvider;

    Task Then_Can_Add_A_New_TILT()
    {
        return Then_Can_Add_A_New_TILT_WithResult(true);
    }
    Task Then_Can_NOT_Add_A_New_TILT()
    {
        return Then_Can_Add_A_New_TILT_WithResult(false);
    }
    async Task Then_Can_Add_A_New_TILT_WithResult(bool result)
    {
        var myTilt = serviceProvider.GetRequiredService<IMyTilts>();
        var data = await myTilt.AddTILT(new TILT_Note_Table(), null);

        var exp = Expect.To.BeNull();
        if(result)
            exp= Expect.To.Not.BeNull();

        Assert.True(exp.Verify(data, ValueFormattingServices.Current),exp.Format(ValueFormattingServices.Current));
        //if (result)
        //    Assert.IsNotNull(data);
        //else
        //    Assert.IsNull(data);
    }
    void SetupTilt(DateTime? oneTILTInTheDB)
    {
        var searchUrl = new Mock<ISearchDataTILT_URL>();
        var insert = new Mock<I_InsertDataApplicationDBContext>();
        insert.Setup(x => x.InsertTILT_Note(It.IsAny<TILT_Note_Table>())).Returns(Task.FromResult(new TILT_Note_Table()));
        var searchNote = new Mock<ISearchDataTILT_Note>();
        TILT_Note[] retArray;
        if (oneTILTInTheDB.HasValue)
        {
            retArray = new TILT_Note[1];
            retArray[0] = new TILT_Note();
            retArray[0].ForDate= oneTILTInTheDB.Value;
        }
        else
        {
            retArray = Array.Empty<TILT_Note>();
        }
        
        searchNote.Setup(it => it.TILT_NoteFind_AsyncEnumerable(It.IsAny<SearchTILT_Note?>())).Returns(retArray.ToAsyncEnumerable());

        var auth = new Mock<IAuthUrl>();
        auth.Setup(x => x.MainUrlId(null)).Returns(1);
        auth.Setup(x => x.MainUrl(null)).Returns(Task.FromResult("ignatandrei"));
        //var q = new SearchDataTILT_URL(null);
        var st = new Mock<IServerTiming>();

        serviceProvider = new ServiceCollection()
        .AddLogging()
        .AddMemoryCache()
        .AddScoped<IServerTiming>(sp=>st.Object )
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
    void Given_Exists_One_TILT_ForDate(DateTime date)
    {
        SetupTilt(date);
    }
    void Given_Today_Is(DateTime date)
    {
        
    }
}

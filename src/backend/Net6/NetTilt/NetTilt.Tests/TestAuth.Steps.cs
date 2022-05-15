namespace NetTilt.Tests;

partial class TestAuth : FeatureFixture
{
    ServiceProvider? serviceProvider;
    int idUrl = 100;
    string jwt = null;
    void SetupTilt(string? url,string? secret, int idUrl)
    {
        var inMemorySettings = new Dictionary<string, string> {
{"TopLevelKey", "TopLevelValue"},
{"MySettings:secretToken", "AnotherKeySettingsForGeneratingJwtForAuthenticationAndAuthorization"},
//...populate as needed for the test
};
        
        IConfiguration configuration = new ConfigurationBuilder()
.AddInMemoryCollection(inMemorySettings)
.Build();

        var searchUrl = new Mock<ISearchDataTILT_URL>();
        if (!string.IsNullOrWhiteSpace(url))
        {
            searchUrl.Setup(x => x.TILT_URLSimpleSearch_URLPart(SearchCriteria.Equal, url)).Returns(
                new TILT_URL[1]
                {
                new TILT_URL()
                {
                    ID=idUrl,
                    Secret=secret,
                    URLPart=url
                }
                }.ToAsyncEnumerable()
                );
        }
        else
        {
            searchUrl.Setup(x => x.TILT_URLSimpleSearch_URLPart(SearchCriteria.Equal, It.IsAny<string>())).Returns(
                Array.Empty<TILT_URL>()
                .ToAsyncEnumerable()
                );
        }
        Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
        //mockSection.Setup(x => x.Value).Returns("ConfigValue");

        var insert = new Mock<I_InsertDataApplicationDBContext>();
        //insert.Setup(x => x.InsertTILT_Note(It.IsAny<TILT_Note_Table>())).Returns(Task.FromResult(new TILT_Note_Table()));


        serviceProvider = new ServiceCollection()
        .AddLogging()
        .AddMemoryCache()
        .AddSingleton<IConfiguration>(configuration)
        .AddScoped<I_InsertDataApplicationDBContext>(sp => insert.Object)
        .AddScoped<ISearchDataTILT_URL>(sp=>searchUrl.Object)
        .AddScoped<IAuthUrl,AuthUrl>()
        .BuildServiceProvider();

    }
    void Given_Wrong_Name()
    {
        Given_An_User_Name_And_Password(null, "test", idUrl);
    }
    void Given_Correct_Name_And_Password()
    {
        Given_An_User_Name_And_Password("test", "test",idUrl);
    }
    void Given_An_User_Name_And_Password(string? url, string? pwd, int idUrl)
    {
        SetupTilt(url, pwd, idUrl);
    }
    async Task Can_Obtain_The_JWT(bool IsNull)
    {
        var data = await serviceProvider!.GetRequiredService<IAuthUrl>().Login("test", "test");
        if (IsNull)
        {
            Assert.IsNull(data);
        }
        else
        {
            Assert.IsNotNull(data);
            jwt = data!;
        }
    }
    Task Then_Can_NOT_Obtain_The_JWT()
    {
        return Can_Obtain_The_JWT(true);
    }
    Task Then_Can_Obtain_The_JWT()
    {
        return Can_Obtain_The_JWT(false);
    }
    void Then_Can_Decrypt_The_JWT()
    {
        var data = serviceProvider!.GetRequiredService<IAuthUrl>().Decrypt(jwt);
        Assert.IsNotNull(data);
        Assert.IsNotEmpty(data!);

    }
    void Then_Can_Verify_The_URL_ID()
    {
        var auth = serviceProvider!.GetRequiredService<IAuthUrl>();
        var data = auth.Decrypt(jwt);
        Assert.IsNotNull(data);
        Assert.IsNotEmpty(data!);
        var urlId = auth.MainUrlId(data);
        Assert.AreEqual(this.idUrl, urlId);

    }
}

﻿namespace NetTilt.Tests;

partial class RealDBTests
{
    protected ServiceProvider? serviceProvider;

    public async Task Then_No_User_IsRegistered()
    {
        var tiltsUrl = serviceProvider.GetRequiredService<IPublicTILTS>();
        var data = await (tiltsUrl.PublicTiltsURL().ToArrayAsync());
        Assert.NotNull(data);
        Assert.IsEmpty(data);
    }
    public async Task Then_No_Login()
    {
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        var data = await auth.Login("asdad", "asdasd");
        Assert.Null(data);
    }

    public async Task When_Those_Users_URL_To_Be_Registerer(InputTable<TILT_URL> urls)
    {
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        foreach (var item in urls)
        {
            await auth.CreateEndpoint(item.URLPart, item.Secret);
        }
    }
    public async Task Then_The_Users_Could_Login(InputTable<TILT_URL> urls)
    {
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        foreach (var item in urls)
        {
            var jwt = await auth.Login(item.URLPart, item.Secret);
            Assert.IsNotEmpty(jwt);
        }
    }
    public async Task And_The_JWT_Can_Be_Decrypted(InputTable<TILT_URL> urls)
    {
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        foreach (var item in urls)
        {
            var jwt = await auth.Login(item.URLPart, item.Secret);
            var c = auth.Decrypt(jwt);
            Assert.IsNotEmpty(c);
        }
    }
    public async Task And_The_JWT_Can_Be_Decrypted_And_Have_Ids(InputTable<TILT_URL> urls)
    {
        List<long> ids = new();
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        foreach (var item in urls)
        {
            var jwt = await auth.Login(item.URLPart, item.Secret);
            var c = auth.Decrypt(jwt);
            var id = auth.MainUrlId(c);
            Assert.IsNotNull(id);
            ids.Add(id.Value);
        }
        var all = Enumerable.Range(1, urls.Count).Select(it => (long)it).ToArray();
        Assert.AreEqual(all, ids.ToArray());
    }
    public async Task And_Exists_Public_Tilts(InputTable<TILT_URL> urls)
    {
        List<long> ids = new();
        var tilts = serviceProvider.GetRequiredService<IPublicTILTS>();
        var data = await (tilts.PublicTiltsURL().ToArrayAsync());
        data = data.OrderBy(it => it).ToArray();

        var all = urls.Select(it => it.URLPart).OrderBy(it => it).ToArray();
        Assert.AreEqual(all, data);
    }
    public async Task And_The_Url_Is_Correct(InputTable<TILT_URL> urls)
    {
        List<long> ids = new();
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        foreach (var item in urls)
        {
            var jwt = await auth.Login(item.URLPart, item.Secret);
            var c = auth.Decrypt(jwt);
            var id = auth.MainUrl(c);
            Assert.IsNotNull(id);
            Assert.AreEqual(item.URLPart, id);
        }
    }

    public async Task And_No_Tilts_Yet(InputTable<TILT_URL> urls)
    {
        List<long> ids = new();
        var tilts = serviceProvider.GetRequiredService<IPublicTILTS>();
        foreach (var item in urls)
        {
            
            var q = await (tilts.LatestTILTs(item.URLPart, 10));
            var data = await q.ToArrayAsync();
            Assert.AreEqual(0, data.Length);
        }
    }
    public async Task Then_The_Users_Have_No_Tilt_Today(InputTable<TILT_URL> urls)
    {
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        var myTilt = serviceProvider.GetRequiredService<IMyTilts>();
        var tz = TimeZoneInfo.Local.ToSerializedString();
        foreach (var item in urls)
        {
            var jwt = await auth.Login(item.URLPart, item.Secret);
            var claim = auth.Decrypt(jwt);
            var existsTilt = await myTilt.HasTILTToday(claim, tz);
            Assert.IsFalse(existsTilt);
        }
    }
    public async Task Then_Public_Tilts_Have_No_Items(InputTable<TILT_URL> urls)
    {
        var publicTilts = serviceProvider.GetRequiredService<IPublicTILTS>();
        foreach (var item in urls)
        {
            var number = await (publicTilts.LatestTILTs(item.URLPart, 1).ToArrayAsync());
            Assert.AreEqual(0, number?.Length ?? 0);
        }
    }
    public async Task Then_Public_Tilts_Have_1_Items(InputTable<TILT_URL> urls)
    {
        var publicTilts = serviceProvider.GetRequiredService<PublicTILTS>();
        foreach (var item in urls)
        {
            var a= await publicTilts.LatestTILTs(item.URLPart, 1);
            
            var number = await (a.ToArrayAsync());
            Assert.AreEqual(1, number?.Length);
        }
    }
    public async Task When_The_Users_Make_A_Tilt(InputTable<TILT_URL> urls)
    {
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        var myTilt = serviceProvider.GetRequiredService<IMyTilts>();

        foreach (var item in urls)
        {
            var jwt = await auth.Login(item.URLPart, item.Secret);
            var claim = auth.Decrypt(jwt);
            var idUrl = auth.MainUrlId(claim).Value;

            var data = await myTilt.AddTILT(new TILT_Note_Table()
            {
                IDURL = idUrl,
                Text = $"A TILT for {item.URLPart}",
            }, claim);
            Assert.GreaterOrEqual(data.ID, 0);
        }
    }
    public async Task Then_The_Users_Have_Tilt_Today(InputTable<TILT_URL> urls)
    {
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        var myTilt = serviceProvider.GetRequiredService<IMyTilts>();
        var tz = TimeZoneInfo.Local.ToSerializedString();
        foreach (var item in urls)
        {
            var jwt = await auth.Login(item.URLPart, item.Secret);
            var claim = auth.Decrypt(jwt);
            var existsTilt = await myTilt.HasTILTToday(claim, tz);
            Assert.IsTrue(existsTilt);
        }
    }
    public async Task And_Have_Tilts(InputTable<TILT_URL> urls)
    {
        List<long> ids = new();
        var tilts = serviceProvider.GetRequiredService<IPublicTILTS>();
        foreach (var item in urls)
        {
            var data = await (tilts.LatestTILTs(item.URLPart, 10).ToArrayAsync());
            Assert.AreEqual(1, data.Length);
        }
    }
    public async Task Then_The_Users_Have_Tilts(InputTable<TILT_URL> urls)
    {
        var auth = serviceProvider.GetRequiredService<IAuthUrl>();
        var myTilt = serviceProvider.GetRequiredService<IMyTilts>();
        foreach (var item in urls)
        {
            var jwt = await auth.Login(item.URLPart, item.Secret);
            var claim = auth.Decrypt(jwt);
            var existsTilt = await myTilt.AllMyTILTs(claim);
            Assert.NotNull(existsTilt);
            Assert.Equals(1, existsTilt!.Length);
        }
    }
}

namespace NetTilt.Auth;

public partial class AuthUrl : IAuthUrl
{
    public const string TokenId = "TokenId";
    private readonly IServerTiming serverTiming;
    private readonly ISearchDataTILT_URL search;
    private readonly I_InsertDataApplicationDBContext insert;
    private string SecretKey;
    public AuthUrl(IServerTiming serverTiming, ISearchDataTILT_URL searchUrl, IConfiguration configuration, I_InsertDataApplicationDBContext insert)
    {

        this.serverTiming = serverTiming;
        search = searchUrl;
        this.insert = insert;
        SecretKey = configuration["MySettings:secretToken"];

    }

    private async Task<string?> privateLogin(string url, string secret)
    {

        var data = await search.TILT_URLSimpleSearch_URLPart(SearchCriteria.Equal, url).ToArrayAsync();
        if (data == null)
            return null;

        if (data.Length != 1)
            return null;

        var item = data[0];
        if (item.Secret != secret)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(TokenId ,item.ID.ToString(),ClaimValueTypes.String ),
                    new Claim(ClaimTypes.Role, "Editor")
                }),

            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var ret = tokenHandler.WriteToken(token);
        return ret;
    }
    public Task<string?> Login(string url, string secret)
    {
        return privateLogin(url, secret);
    }
    private async Task<string?> privateCreateEndpoint(string url, string secret)
    {
        var data = await search.TILT_URLSimpleSearch_URLPart(SearchCriteria.Equal, url).ToArrayAsync();
        if (data?.Length > 0)
        {
            //maybe he wants to login ?
            return await privateLogin(url, secret);
        }

        await insert.InsertTILT_URL(new TILT_URL_Table() { Secret = secret, URLPart = url });
        return await privateLogin(url, secret);

    }
    public Task<string?> CreateEndpoint(string url, string secret)
    {
        return privateCreateEndpoint(url, secret);
    }
    private Claim[]? privateDecrypt(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        if ((jwtSecurityToken?.Claims?.Count() ?? 0) == 0)
        {
            return null;
        }
        var claims = jwtSecurityToken!.Claims!.ToArray();
        var webPage = claims?.FirstOrDefault(it => it.Type == TokenId);
        if (webPage == null)
        {
            return null;

        }
        claims = claims?.Where(it => (it.Type == TokenId || it.Type == "role")).ToArray();
        return claims;
    }
    public Claim[]? Decrypt(string token)
    {
        return privateDecrypt(token);
    }
    public long? MainUrlId(Claim[]? claims)
    {
        return privateMainUrlId(claims);
    }

    private long? privateMainUrlId(Claim[]? claims)
    {
        if (claims == null)
            return null;
        var c = claims?.FirstOrDefault(it => it.Type == TokenId);
        if (c == null)
            return null;

        return long.Parse(c.Value);
    }


    public Task<string?> MainUrl(Claim[]? claims)
    {
        return privateMainUrl(claims);
    }

    private async Task<string?> privateMainUrl(Claim[]? claims)
    {
        var id = MainUrlId(claims);
        if (id == null)
            return null;
        var s = await search.TILT_URLSimpleSearch_ID(SearchCriteria.Equal, id.Value).ToArrayAsync();
        if (s.Length != 1)
            return null;
        return s[0].URLPart;
    }

}


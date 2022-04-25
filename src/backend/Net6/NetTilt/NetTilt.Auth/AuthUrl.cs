namespace NetTilt.Auth;
public class AuthUrl
{
    private readonly ISearchDataTILT_URL search;

    public AuthUrl(ISearchDataTILT_URL searchUrl)
    {
        this.search = searchUrl;
    }
    public async Task<string?> Login(string url, string secret)
    {
        var data =await search.TILT_URLSimpleSearch_URLPart(SearchCriteria.Equal, url).ToArrayAsync(); ;
        if (data == null)
            return null;

        if(data.Length != 0)
            return null;

        var item = data[0]!;
        if (item.Secret != secret)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("andrei");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Webpage,item.ID.ToString() ),
                    new Claim(ClaimTypes.Role, "Editor")
                }),

            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var ret=  tokenHandler.WriteToken(token);
        return ret;
    }

}

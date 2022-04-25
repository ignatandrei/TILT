using Generated;

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

        if(data .Length != 0)
            return null;

        var item = data[0]!;
        if (item.Secret != secret)
            return null;

        return item.ID.ToString();
    }

}

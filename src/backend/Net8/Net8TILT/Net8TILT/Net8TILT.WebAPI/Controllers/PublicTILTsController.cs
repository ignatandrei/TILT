namespace NetTilt.WebAPI.Controllers;
[EnableRateLimiting("UnlimitMeAndLocalHost")]
[AllowAnonymous]
[Route("api/[controller]/[action]")]
[ApiController]
public class PublicTILTsController : ControllerBase
{
    private readonly IServerTiming serverTiming;
    private readonly ISearchDataTILT_URL searchUrl;
    private readonly IPublicTILTS publicTILTS;

    public PublicTILTsController([FromServices]IServerTiming serverTiming, [FromServices] ISearchDataTILT_URL searchUrl, IPublicTILTS publicTILTS)
    {
        this.serverTiming = serverTiming;
        this.searchUrl = searchUrl;
        this.publicTILTS = publicTILTS;
    }
    [HttpGet]
    public IAsyncEnumerable<string> PublicTiltsURL()
    {

        return publicTILTS.PublicTiltsURL();
    }
    [HttpGet("{urlPart}/{numberTILTS}")]
    public async IAsyncEnumerable<TILT_Note_Table> LatestTILTs(string urlPart, int numberTILTS, [FromServices] ISearchDataTILT_Note searchNotes)
    {
        Console.WriteLine("LatestTilts");
        var data =  publicTILTS.LatestTILTs(urlPart,numberTILTS);
        
        if (data== null)
        {
            throw new Exception("No data found");
        }
        Console.WriteLine("starting");
        long i=0;
        await foreach (var item in data)
        {
            i++;
            if(i % 100 == 0)
            {
                await Task.Delay(2500);
                Console.WriteLine("Delaying" +DateTime.Now.ToString("HHmmss"));
            }
            yield return item;
        }
        await Task.Delay(2500);
        Console.WriteLine("ending");
    }
    [HttpGet("{urlPart}/count")]
    public async Task<long> CountTILTs(string urlPart,[FromServices] ISearchDataTILT_Note searchNotes)
    {
        var data = await publicTILTS.Count(urlPart);
        return data;
    }
}

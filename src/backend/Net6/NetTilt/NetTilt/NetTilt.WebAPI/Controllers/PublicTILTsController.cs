using NetTilt.Logic;

namespace NetTilt.WebAPI.Controllers;

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
    public ActionResult<IAsyncEnumerable<TILT_Note_Table>> LatestTILTs(string urlPart, int numberTILTS, [FromServices] ISearchDataTILT_Note searchNotes)
    {
        var data =  publicTILTS.LatestTILTs(urlPart,numberTILTS);
        
        if (data== null)
        {
            return new NotFoundObjectResult($"cannot find {urlPart}");
        }

        return Ok(data);
    }
    [HttpGet("{urlPart}/count")]
    public async Task<long> CountTILTs(string urlPart,[FromServices] ISearchDataTILT_Note searchNotes)
    {
        var data = await publicTILTS.Count(urlPart);
        return data;
    }
}

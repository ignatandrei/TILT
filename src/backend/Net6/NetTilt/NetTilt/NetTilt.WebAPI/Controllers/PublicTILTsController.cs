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
    public async Task<ActionResult<TILT_Note_Table[]?>> LatestTILTs(string urlPart, int numberTILTS, [FromServices] ISearchDataTILT_Note searchNotes)
    {
        //TB: 2022-06-11 to be moved into a class - skinny controllers
        var data = await publicTILTS.LatestTILTs(urlPart,numberTILTS);

        if (data== null)
        {
            return new NotFoundObjectResult($"cannot find {urlPart}");
        }

        return data;
    }
}

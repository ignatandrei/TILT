
namespace NetTilt.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TILTController : ControllerBase
{
    private readonly IMyTilts addLogic;
    private readonly IAuthUrl auth;

    public TILTController(IMyTilts addLogic, IAuthUrl auth)
    {
        this.addLogic = addLogic;
        this.auth = auth;
    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpPost]
    public async Task<ActionResult<TILT_Note_Table>> AddTILT(TILT_Note_Table note)
    {
        var data =await addLogic.AddTILT(note, this.User?.Claims.ToArray());
        if(data == null)
        {
            return new BadRequestObjectResult("add tilt was not succesfull");
        }
        return data;
    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpGet()]
    public async Task<ActionResult<bool>> HasTILTToday([FromServices] ISearchDataTILT_Note searchNotes)
    {
        var data = await addLogic.HasTILTToday(this.User?.Claims.ToArray());
        return data;
    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpGet()]
    public async Task<ActionResult<TILT_Note_Table[]?>> AllMyTILTs([FromServices] ISearchDataTILT_Note searchNotes)
    {
        var data = await addLogic.AllMyTILTs(this.User?.Claims.ToArray());

        if (data == null)
        {
            return new UnauthorizedResult();
        }
        return data;
    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpGet("{numberTILTS}")]
    public async Task<ActionResult<TILT_Note_Table[]?>> MyLatestTILTs(int numberTILTS, [FromServices] ISearchDataTILT_Note searchNotes)
    {
        var data = await addLogic.MyLatestTILTs(numberTILTS, this.User?.Claims.ToArray());

        if (data == null)
        {
            return new UnauthorizedResult();
        }
        return data;
    }

    [Authorize(Policy = "CustomBearer")]
    [HttpGet]
    public ActionResult<long> WebPageID()
    {

        var data = auth.MainUrlId(this.User?.Claims?.ToArray());
        if (!data.HasValue)
            return NotFound("cannot find id from claims");

        return data;
    }

    [Authorize(Policy = "CustomBearer")]
    [HttpGet]
    public async Task<ActionResult<string>> MainUrl()
    {

        var data = await auth.MainUrl(this.User?.Claims?.ToArray());
        if (string.IsNullOrWhiteSpace(data))
            return NotFound("cannot find webpage from claims");

        return data!;
    }
}


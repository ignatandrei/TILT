
namespace NetTilt.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TILTController : ControllerBase
{
    private readonly IMyTilts addLogic;
    

    public TILTController(IMyTilts addLogic)
    {
        this.addLogic = addLogic;        
    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpPost]
    public async Task<ActionResult<TILT_Note_Table>> AddTILT(TILT_Note_Table note)
    {
        var data =await addLogic.AddTILT(note, this.User?.Claims.ToArray());
        if(data == null)
        {
            return new UnauthorizedResult();
        }
        return data;
    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpGet()]
    public async Task<ActionResult<bool?>> HasTILTToday([FromServices] ISearchDataTILT_Note searchNotes)
    {
        //TB: 2022-05-11 to be moved into a class - skinny controllers
        var all = await AllMyTILTs(searchNotes);
        if (all?.Value == null)
            return false;
        var now = DateTime.UtcNow.Date;
        return all.Value.FirstOrDefault (it=>it.ForDate.HasValue && now.Subtract(it.ForDate.Value.Date).TotalDays == 0) !=null;
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
}


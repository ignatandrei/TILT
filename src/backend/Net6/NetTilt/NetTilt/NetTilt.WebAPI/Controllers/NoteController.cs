namespace NetTilt.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly IAuthUrl auth;

    public NoteController(IAuthUrl auth)
    {
        this.auth = auth;
    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpPost]
    public async Task<ActionResult<TILT_Note_Table>> AddNote([FromServices] I_InsertDataApplicationDBContext insert, TILT_Note_Table note)
    {
        var c = this.User?.Claims.ToArray();
        var idUrl = auth.MainUrlId(c);
        if (idUrl == null)
        {
            return new UnauthorizedResult();
        }
        note.IDURL = idUrl.Value;
        note.ID = 0;
        note.ForDate = DateTime.UtcNow;
        var noteOrig = new TILT_Note();
        noteOrig.CopyFrom(note);
        await insert.InsertTILT_Note(noteOrig);
        note.CopyFrom(noteOrig);
        return note;

    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpGet()]
    public async Task<ActionResult<bool?>> HasPostedToday([FromServices] ISearchDataTILT_Note searchNotes)
    {
        //Tobemoved
        var all = await AllMyNotes(searchNotes);
        if (all?.Value == null)
            return false;
        var now = DateTime.UtcNow.Date;
        return all.Value.FirstOrDefault (it=>it.ForDate.HasValue && now.Subtract(it.ForDate.Value.Date).TotalDays == 0) !=null;
    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpGet()]
    public async Task<ActionResult<TILT_Note_Table[]?>> AllMyNotes([FromServices] ISearchDataTILT_Note searchNotes)
    {
        //Tobemoved
        var c = this.User?.Claims.ToArray();
        var idUrl = auth.MainUrlId(c);
        if (idUrl == null)
        {
            return new UnauthorizedResult();
        }
        var data =await searchNotes.TILT_NoteSimpleSearch_IDURL(SearchCriteria.Equal, idUrl.Value).ToArrayAsync();
        var ret= data.Select(it => { var n = new TILT_Note_Table(); n.CopyFrom(it);return n; }).ToArray();
        return ret;
    }
}


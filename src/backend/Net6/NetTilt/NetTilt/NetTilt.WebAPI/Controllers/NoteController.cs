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
        await insert.InsertTILT_Note(note);
        return note;

    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpGet("{number}")]
    public async Task<ActionResult<TILT_Note_Table[]?>> MyLastNotes(long number,[FromServices] ISearchDataTILT_Note searchNotes)
    {
        var c = this.User?.Claims.ToArray();
        var idUrl = auth.MainUrlId(c);
        if (idUrl == null)
        {
            return new UnauthorizedResult();
        }
        var data =await searchNotes.TILT_NoteSimpleSearch_IDURL(SearchCriteria.Equal, idUrl.Value).ToArrayAsync();
        return data;
    }
}


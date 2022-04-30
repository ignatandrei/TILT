namespace NetTilt.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TILTController : ControllerBase
{
    private readonly IAuthUrl auth;

    public TILTController(IAuthUrl auth)
    {
        this.auth = auth;
    }
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpPost]
    public async Task<ActionResult<TILT_Note_Table>> AddTILT([FromServices] I_InsertDataApplicationDBContext insert, TILT_Note_Table note)
    {
        //TB: 2022-05-01 to be moved into a class - skinny controllers
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
    public async Task<ActionResult<bool?>> HasTILTToday([FromServices] ISearchDataTILT_Note searchNotes)
    {
        //TB: 2022-05-01 to be moved into a class - skinny controllers
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
        //TB: 2022-05-01 to be moved into a class - skinny controllers
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
    [Authorize(Policy = "CustomBearer", Roles = "Editor")]
    [HttpGet("{numberTILTS}")]
    public async Task<ActionResult<TILT_Note_Table[]?>> MyLatestTILTs(int numberTILTS, [FromServices] ISearchDataTILT_Note searchNotes)
    {
        //TB: 2022-05-01 to be moved into a class - skinny controllers
        var c = this.User?.Claims.ToArray();
        var idUrl = auth.MainUrlId(c);
        if (idUrl == null)
        {
            return new UnauthorizedResult();
        }
        SearchTILT_Note search = new();
        search.SearchFields = new SearchField<eTILT_NoteColumns>[1];
        search.SearchFields[0] = new SearchField<eTILT_NoteColumns>
        {
            FieldName = eTILT_NoteColumns.IDURL,
            Value = idUrl.ToString(),
            Criteria = SearchCriteria.Equal
        };
        search.OrderBys = new OrderBy<eTILT_NoteColumns>[1];
        search.OrderBys[0] = new OrderBy<eTILT_NoteColumns>()
        {
            FieldName = eTILT_NoteColumns.ID,
            Asc = false
        };
        search.PageNumber = 1;
        search.PageSize = numberTILTS;
        var data = await searchNotes.TILT_NoteFind_AsyncEnumerable(search).ToArrayAsync();
        var ret = data.Select(it => { var n = new TILT_Note_Table(); n.CopyFrom(it); return n; }).ToArray();
        return ret;
    }
}


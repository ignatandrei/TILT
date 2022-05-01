namespace NetTilt.WebAPI.Controllers;

[AllowAnonymous]
[Route("api/[controller]/[action]")]
[ApiController]
public class PublicTILTsController : ControllerBase
{
    private readonly ISearchDataTILT_URL searchUrl;

    public PublicTILTsController([FromServices] ISearchDataTILT_URL searchUrl)
    {
        this.searchUrl = searchUrl;
    }
    [HttpGet]
    public IAsyncEnumerable<string> PublicTiltsURL()
    {
        SearchTILT_URL search = new ();
        search.PageSize = int.MaxValue;
        return searchUrl.TILT_URLFind_AsyncEnumerable(search).Select(it => it.URLPart);

    }
    [HttpGet("{urlPart}/{numberTILTS}")]
    public async Task<ActionResult<TILT_Note_Table[]?>> LatestTILTs(string urlPart, int numberTILTS, [FromServices] ISearchDataTILT_Note searchNotes)
    {        
        //TB: 2022-05-02 to be moved into a class - skinny controllers
        var dataUrls = await searchUrl.TILT_URLSimpleSearch_URLPart(SearchCriteria.Equal, urlPart).ToArrayAsync();

        if ((dataUrls?.Length ?? 0) == 0)
        {
            return new NotFoundObjectResult($"cannot find {urlPart}");
        }
        if (dataUrls!.Length != 1)
        {
            return new NotFoundObjectResult($"too much  {urlPart} ");
        }

        var idUrl = dataUrls![0].ID;
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

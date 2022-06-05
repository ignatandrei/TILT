
namespace NetTilt.Logic
{
    [AutoMethods(CustomTemplateFileName = "../AutoMethod.txt", MethodPrefix = "auto", template = TemplateMethod.CustomTemplateFile)]
    [AutoGenerateInterface]
    public partial class PublicTILTS : IPublicTILTS
    {
        private readonly IServerTiming serverTiming;
        private readonly IMemoryCache cache;
        private readonly ISearchDataTILT_Note searchNotes;
        private readonly ISearchDataTILT_URL searchUrl;

        public PublicTILTS(IServerTiming serverTiming, IMemoryCache cache, ISearchDataTILT_Note searchNotes, ISearchDataTILT_URL searchUrl)
        {
            this.serverTiming = serverTiming;
            this.cache = cache;
            this.searchNotes = searchNotes;
            this.searchUrl = searchUrl;
       }
        public IAsyncEnumerable<string> PublicTiltsURL()
        {
            return privatePublicTiltsURL();
        }
        [AOPMarkerMethod]
        private IAsyncEnumerable<string> privatePublicTiltsURL()
        {
            SearchTILT_URL search = new();
            search.PageSize = int.MaxValue;
            return searchUrl.TILT_URLFind_AsyncEnumerable(search).Select(it => it.URLPart);

        }

        public Task<TILT_Note_Table[]?> LatestTILTs(string urlPart, int numberTILTS)
        {
            return privateLatestTILTs(urlPart, numberTILTS);
        }
        [AOPMarkerMethod]
        private async Task<TILT_Note_Table[]?> privateLatestTILTs(string urlPart, int numberTILTS)
        {
            if (cache.TryGetValue<TILT_Note_Table[]>(urlPart, out var result))
            {
                return result;
            }


            var dataUrls = await searchUrl.TILT_URLSimpleSearch_URLPart(SearchCriteria.Equal, urlPart).ToArrayAsync();

            if ((dataUrls?.Length ?? 0) == 0)
            {
                return null;
            }
            if (dataUrls!.Length != 1)
            {
                return null;
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
            cache.Set(urlPart, ret,DateTimeOffset.Now.AddDays(10));
            return ret;
        }
    }
}

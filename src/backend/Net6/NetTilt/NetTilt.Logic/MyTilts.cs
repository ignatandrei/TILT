using AOPMethodsCommon;
using BoilerplateFree;
using Generated;
using NetTilt.Auth;
using System.Security.Claims;

namespace NetTilt.Logic
{
    [AutoGenerateInterface]
    [AutoMethods(CustomTemplateFileName = "../AutoMethod.txt", MethodPrefix = "auto", template = TemplateMethod.CustomTemplateFile)]
    public partial class MyTilts: IMyTilts
    {
        private readonly I_InsertDataApplicationDBContext insert;
        private readonly IAuthUrl auth;
        private readonly ISearchDataTILT_Note searchNotes;

        public MyTilts(I_InsertDataApplicationDBContext insert, IAuthUrl auth, ISearchDataTILT_Note searchNotes)
        {
            this.insert = insert;
            this.auth = auth;
            this.searchNotes = searchNotes;
        }
        
        public Task<TILT_Note_Table?> AddTILT(TILT_Note_Table note, Claim[]? c)
        {
            return privateAddTILT(note, c);
        }
        [AOPMarkerMethod]
        private async Task<TILT_Note_Table?> privateAddTILT(TILT_Note_Table note, Claim[]? c)
        {
            var idUrl = auth.MainUrlId(c);
            if (idUrl == null)
            {
                return null;
            }
            if (await HasTILTToday(c))
            {
                return null;
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
        
        public Task<TILT_Note_Table[]?> AllMyTILTs(Claim[]? c)
        {
            return privateAllMyTILTs(c);
        }
        [AOPMarkerMethod]
        private async Task<TILT_Note_Table[]?> privateAllMyTILTs(Claim[]? c)
        {

            var idUrl = auth.MainUrlId(c);
            if (idUrl == null)
            {
                return null;
            }
            var data = await searchNotes.TILT_NoteSimpleSearch_IDURL(SearchCriteria.Equal, idUrl.Value).ToArrayAsync();
            var ret = data.Select(it => { var n = new TILT_Note_Table(); n.CopyFrom(it); return n; }).ToArray();
            return ret;
        }

        public Task<bool> HasTILTToday(Claim[]? c)
        {
            return privateHasTILTToday(c);
        }
        [AOPMarkerMethod]
        async Task<bool> privateHasTILTToday(Claim[]? c)
        {
            var all = await MyLatestTILTs(1, c);
            if (all == null)
                return false;
            var now = DateTime.UtcNow.Date;
            return all.FirstOrDefault(it => it.ForDate.HasValue && now.Subtract(it.ForDate.Value.Date).TotalDays == 0) != null;
        }
        public Task<TILT_Note_Table[]?> MyLatestTILTs(int numberTILTS, Claim[]? c)
        {
            return privateMyLatestTILTs(numberTILTS, c);
        }
        [AOPMarkerMethod]
        private async Task<TILT_Note_Table[]?> privateMyLatestTILTs(int numberTILTS, Claim[]? c)
        {

            var idUrl = auth.MainUrlId(c);
            if (idUrl == null)
            {
                return null;
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


        Task<string?> GetMainUrl(Claim[]? c)
        {
            return auth.MainUrl(c);
        }
        
    }
}
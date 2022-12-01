
using System;

namespace NetTilt.Logic
{
    [AutoGenerateInterface]
    [AutoMethods(CustomTemplateFileName = "../AutoMethod.txt", MethodPrefix = "auto", template = TemplateMethod.CustomTemplateFile)]
    public partial class MyTilts: IMyTilts
    {
        private readonly IServerTiming serverTiming;
        private readonly IMemoryCache cache;
        private readonly I_InsertDataApplicationDBContext insert;
        private readonly IAuthUrl auth;
        private readonly ISearchDataTILT_Note searchNotes;

        public MyTilts(IServerTiming serverTiming, IMemoryCache cache, I_InsertDataApplicationDBContext insert, IAuthUrl auth, ISearchDataTILT_Note searchNotes)
        {
            this.serverTiming = serverTiming;
            this.cache = cache;
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
            if (await HasTILTToday(c,note.TimeZoneString))
            {
                return null;
            }
            var url = await auth.MainUrl(c);
            if(string.IsNullOrWhiteSpace( url))
            {
                return null;
            }
            //verify if there is a timezone
            var tz = TimeZoneInfo.FindSystemTimeZoneById(note.TimeZoneString);
            if (tz == null)
                throw new TimeZoneNotFoundException("cannot find " + note.TimeZoneString);
            
            var ser = tz.ToSerializedString();
            if(string.IsNullOrWhiteSpace(ser))
                throw new TimeZoneNotFoundException("cannot serialize " + note.TimeZoneString);



            note.IDURL = idUrl.Value;
            note.ID = 0;
            note.ForDate = DateTime.UtcNow;
            var noteOrig = new TILT_Note_Table();
            noteOrig.CopyFrom(note);
            await insert.InsertTILT_Note(noteOrig);
            note.CopyFrom(noteOrig);
            cache.Remove(url);
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

        public Task<bool> HasTILTToday(Claim[]? c, string timezone)
        {
            return privateHasTILTToday(c, timezone);
        }
        [AOPMarkerMethod]
        async Task<bool> privateHasTILTToday(Claim[]? c, string timeZone)
        {
            var all = await MyLatestTILTs(1, c);
            if (all == null)
                return false;
            if (all.Length == 0)
                return false;
            //var now = DateTime.UtcNow.Date;
            var  it = all[0];

            var dateNowUTC = DateTime.UtcNow;
            TimeZoneInfo? tz = null;
            try
            {
                tz = TimeZoneInfo.FromSerializedString(timeZone);
            }
            catch (Exception)
            {
                //TODO: log
                try
                {
                    tz = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                }
                catch (Exception)
                {
                    //TODO:log
                }
            }
            if (tz == null)
                throw new TimeZoneNotFoundException(" cannote deserialize " + timeZone);
            
            var localTimeNow= TimeZoneInfo.ConvertTimeFromUtc(dateNowUTC, tz);
            var data = it.ForDate ?? DateTime.UtcNow;
            var localTimeWhenPosted = TimeZoneInfo.ConvertTimeFromUtc(data, tz);

            return ( localTimeNow.Date.Subtract(localTimeWhenPosted.Date).TotalDays == 0) ;
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
        
    }
}
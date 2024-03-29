﻿namespace NetTilt.Logic;
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

    private IAsyncEnumerable<string> privatePublicTiltsURL()
    {
        SearchTILT_URL search = new();
        search.PageSize = int.MaxValue;
        return searchUrl.TILT_URLFind_AsyncEnumerable(search).Select(it => it.URLPart);

    }

    public IAsyncEnumerable<TILT_Note_Table>? LatestTILTs(string urlPart, int numberTILTS)
    {
        return privateLatestTILTs(urlPart, numberTILTS);

    }
    private async Task<SearchTILT_Note?> SearchFromUrlPart(string urlPart, int numberTILTS)
    {

        var dataUrls = await searchUrl.TILT_URLSimpleSearch_URLPart(SearchCriteria.Equal, urlPart).ToArrayAsync();

        if (dataUrls?.Length < 1)
            return null;
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
        return search;
    }
    public Task<long> Count(string urlPart)
    {
        return privateCount(urlPart);
    }
    private async Task<long> privateCount(string urlPart)
    {
        if (cache.TryGetValue<long>(nameof(privateCount) + urlPart, out var result))
        {
            //why I cant return result.ToAsyncEnumerable() ?
            return result;
        }
        var search = await SearchFromUrlPart(urlPart, int.MaxValue - 1);
        var dataFromDB = searchNotes.TILT_NoteFind_AsyncEnumerable(search);
        var ret = await dataFromDB.LongCountAsync();
        return ret;

    }
    private async IAsyncEnumerable<TILT_Note_Table> privateLatestTILTs(string urlPart, int numberTILTS)
    {
        var rand = new Random(DateTime.Now.Millisecond);
        int nr = 0;
        if (cache.TryGetValue<TILT_Note_Table[]>(urlPart, out var result))
        {
            //why I cant return result.ToAsyncEnumerable() ?
            await foreach (var item in result.ToAsyncEnumerable())
            {
                //if (nr > 0 && nr % 10 == 0)
                //    await Task.Delay(2500);
                //nr++;
                //await Task.Delay(rand.Next(2,1000));
                yield return item;
            }
        }
        var search = await SearchFromUrlPart(urlPart, numberTILTS);
        var ret = new List<TILT_Note_Table>();

        var dataFromDB = searchNotes.TILT_NoteFind_AsyncEnumerable(search);

        var data = dataFromDB.Select(it => { var n = new TILT_Note_Table(); n.CopyFrom(it); return n; });
        await foreach (var it in data)
        {
            //if (nr > 0 && nr % 10 == 0)
            //    await Task.Delay(2500);
            nr++;
            //await Task.Delay(rand.Next(2, 1000));
            yield return it;
        }
        //var ret = 
        cache.Set(urlPart, ret.ToArray(), DateTimeOffset.Now.AddDays(10));
        //return ret.ToAsyncEnumerable();
    }
}

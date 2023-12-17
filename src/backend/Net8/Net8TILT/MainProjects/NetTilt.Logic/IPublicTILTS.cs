
namespace NetTilt.Logic;

public interface IPublicTILTS
{
    Task<long> Count(string urlPart);
    IAsyncEnumerable<TILT_Note_Table>? LatestTILTs(string urlPart, int numberTILTS);
    IAsyncEnumerable<string> PublicTiltsURL();
}
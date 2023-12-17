
namespace NetTilt.Logic;

public interface IMyTilts
{
    Task<TILT_Note_Table?> AddTILT(TILT_Note_Table note, Claim[]? c);
    Task<TILT_Note_Table[]?> AllMyTILTs(Claim[]? c);
    Task<bool> HasTILTToday(Claim[]? c, string timezone);
    Task<TILT_Note_Table[]?> MyLatestTILTs(int numberTILTS, Claim[]? c);
}
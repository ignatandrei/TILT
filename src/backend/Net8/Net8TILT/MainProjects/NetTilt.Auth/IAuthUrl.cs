
namespace NetTilt.Auth;

public interface IAuthUrl
{
    Task<string?> CreateEndpoint(string url, string secret);
    Claim[]? Decrypt(string token);
    Task<string?> Login(string url, string secret);
    Task<string?> MainUrl(Claim[]? claims);
    long? MainUrlId(Claim[]? claims);
}
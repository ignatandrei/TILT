namespace NetTilt.Auth;

public class TheSecretToken
{
    public TheSecret(IConfiguration configuration)
    {
        Secret = configuration["MySettings:secretToken" ?? "andrei";
    }
    public string Secret { get; }
}

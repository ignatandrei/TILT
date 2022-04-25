namespace NetTilt.Auth;

public class TheSecretToken
{
    public TheSecretToken(IConfiguration configuration)
    {
        Secret = configuration["MySettings:secretToken" ];
    }
    public string Secret { get; }
}

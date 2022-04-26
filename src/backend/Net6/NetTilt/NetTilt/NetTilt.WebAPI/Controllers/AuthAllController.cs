namespace NetTilt.WebAPI.Controllers;

[AllowAnonymous]
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthAllController : ControllerBase
{
    private readonly IAuthUrl auth;

    public AuthAllController(IAuthUrl auth)
    {
        this.auth = auth;
    }
    [HttpGet("{urlPart}/{secret}")]
    public async Task<ActionResult<string>> Login( string urlPart, string secret)
    {
        var data= await auth.Login(urlPart, secret);
        if (string.IsNullOrWhiteSpace(data))
        {
            return new NotFoundObjectResult($"cannot find {urlPart} for {secret}");
        }
        return data;
    }
    [HttpPost("{token}")]
    public ActionResult<int> Decrypt(string token)
    {
        //return 0;
        var data = auth.Decrypt(token);
        if (data == null)
        {
            return new NotFoundObjectResult($"cannot find {token}");
        }
        return data;
    }
}


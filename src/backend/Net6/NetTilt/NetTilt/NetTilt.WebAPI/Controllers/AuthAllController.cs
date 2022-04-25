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
}


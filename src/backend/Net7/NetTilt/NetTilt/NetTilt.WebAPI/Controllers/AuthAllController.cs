using System.Security.Claims;

namespace NetTilt.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthAllController : ControllerBase
{
    private readonly IAuthUrl auth;

    public AuthAllController(IAuthUrl auth)
    {
        this.auth = auth;
        
    }
    [AllowAnonymous]
    [HttpGet("{urlPart}/{secret}")]
    public async Task<ActionResult<string>> CreateEndPoint(string urlPart, string secret)
    {
        var data = await auth.CreateEndpoint(urlPart, secret);
        if (string.IsNullOrWhiteSpace(data))
        {
            return new NotFoundObjectResult($"{urlPart} already exists or other error");
        }
        return data;
    }
    [AllowAnonymous]
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
    [AllowAnonymous]
    [HttpPost("{token}")]
    public ActionResult<Claim[]> Decrypt(string token)
    {
        //return 0;
        var data = auth.Decrypt(token);
        if (data == null)
        {
            return new NotFoundObjectResult($"cannot find {token}");
        }
        return data;
    }

    

    [Authorize(Policy = "CustomBearer")]
    [HttpGet]
    public bool IsUserAuthenticated()
    {
        return true;
    }
}



namespace NetTilt.Auth
{
    [AutoMethods(CustomTemplateFileName = "../AutoMethod.txt", MethodPrefix = "auto", template = TemplateMethod.CustomTemplateFile)]
    [AutoGenerateInterface]
    public partial class AuthUrl : IAuthUrl
    {
        const string TokenId = "TokenId";
        private readonly ISearchDataTILT_URL search;
        private readonly I_InsertDataApplicationDBContext insert;
        private string SecretKey;
        public AuthUrl(ISearchDataTILT_URL searchUrl, IConfiguration configuration, I_InsertDataApplicationDBContext insert)
        {
            search = searchUrl;
            this.insert = insert;
            SecretKey = configuration["MySettings:secretToken"];
        }
        [AOPMarkerMethod]
        private async Task<string?> privateLogin(string url, string secret)
        {
            var data = await search.TILT_URLSimpleSearch_URLPart(SearchCriteria.Equal, url).ToArrayAsync(); ;
            if (data == null)
                return null;

            if (data.Length != 1)
                return null;

            var item = data[0];
            if (item.Secret != secret)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(TokenId ,item.ID.ToString() ),
                    new Claim(ClaimTypes.Role, "Editor")
                    }),

                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var ret = tokenHandler.WriteToken(token);
            return ret;
        }
        public Task<string?> Login(string url, string secret)
        {
            return privateLogin(url, secret);
        }
        [AOPMarkerMethod]
        private async Task<string?> privateCreateEndpoint(string url, string secret)
        {
            var data = await search.TILT_URLSimpleSearch_URLPart(SearchCriteria.Equal, url).ToArrayAsync(); ;
            if (data?.Length>0)
            {
                //maybe he wants to login ?
                return await privateLogin(url, secret);
            }

            var val = await insert.InsertTILT_URL(new TILT_URL_Table() { Secret=secret, URLPart=url});
            return await privateLogin(url, secret);

        }
        public Task<string?> CreateEndpoint(string url, string secret)
        {
            return privateCreateEndpoint(url, secret);
        }
        public Claim[]? Decrypt(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new ();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            if (jwtSecurityToken == null)
            {
                return null;
            }
            if ((jwtSecurityToken.Claims?.Count() ?? 0)== 0)
            {
                return null;
            }
            var claims = jwtSecurityToken.Claims?.ToArray();
            var webPage = claims?.FirstOrDefault(it => it.Type == TokenId);
            if (webPage == null)
            {
                return null;

            }
            claims = claims?.Where(it => (it.Type == TokenId || it.Type == "role")).ToArray();
            return claims;
        }
        public long? MainUrlId(Claim[] claims)
        {
            var c = claims?.FirstOrDefault(it => it.Type == TokenId);
            if (c == null)
                return null;

            return long.Parse(c.Value);
        }
        
    }
}

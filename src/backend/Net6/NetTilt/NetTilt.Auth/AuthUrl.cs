namespace NetTilt.Auth
{

    [AutoGenerateInterface]
    public class AuthUrl : IAuthUrl
    {
        private readonly ISearchDataTILT_URL search;
        private string SecretKey;
        public AuthUrl(ISearchDataTILT_URL searchUrl, IConfiguration configuration)
        {
            search = searchUrl;
            SecretKey = configuration["MySettings:secretToken"];
        }
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
                    new Claim(ClaimTypes.Webpage,item.ID.ToString() ),
                    new Claim(ClaimTypes.Role, "Editor")
                    }),

                Expires = DateTime.UtcNow.AddMinutes(5),
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

    }
}
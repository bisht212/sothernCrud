using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelAccomodationAPI.TokenCreateClass.InterFaces;

namespace TravelAccomodationAPI.TokenCreateClass
{
    public class Token : IToken
    {
        private readonly IConfiguration _config;
        public Token(IConfiguration config)
        {
                _config = config;
        }
        public string CreateToken(TokenModel data)
        {
            var claims = new[]
            {
                new Claim("username", data.FirstName + " "+ data.LastName),
                new Claim("email", data.Email)
            };


            //  Get key from appsettings.json
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds);

            return   new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

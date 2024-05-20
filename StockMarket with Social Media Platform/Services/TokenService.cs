using Microsoft.IdentityModel.Tokens;
using StockMarket_with_Social_Media_Platform.Interfaces;
using StockMarket_with_Social_Media_Platform.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockMarket_with_Social_Media_Platform.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        public string CreateToken(AppUser user)
        {
            // Create claims to be included in the Token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            // Set what type of encryption to be used
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Create a token as an object representation
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Wrap into claims identity
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            // Call the token handler to create the actual token
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //return the token in the form of a string
            return tokenHandler.WriteToken(token);
        }
    }
}

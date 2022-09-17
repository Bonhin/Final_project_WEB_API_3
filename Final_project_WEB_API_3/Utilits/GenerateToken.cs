using Final_project_WEB_API_3.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Final_project_WEB_API_3.Utilits
{
    public class GenerateToken
    {
        private readonly TokenConfiguration _configuration;

        public GenerateToken(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwt(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.Secret));
            var tokenHandler = new JwtSecurityTokenHandler();

            var moduleClaim = new Claim(ClaimValueTypes.String, _configuration.Module);
            var subjectClaim = new Claim(JwtRegisteredClaimNames.Sub, _configuration.Subject);
            List<Claim> claims = new List<Claim>();
            claims.Add(subjectClaim);
            claims.Add(moduleClaim);

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_configuration.ExpirationTimeInHours),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return tokenHandler.WriteToken(jwtToken);
        }
    }
}

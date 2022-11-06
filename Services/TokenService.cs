using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class TokenService
    {
        #region Dependency Injection
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region JWT Tools
        public string GenerateJWT(User user)
        {
            // CREO EL HEADER
            SymmetricSecurityKey _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:SECRET_KEY"])
                );
            SigningCredentials _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            JwtHeader _Header = new JwtHeader(_signingCredentials);

            // CREO LOS CLAIMS
            Claim[] _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim("Name", user.Name!),
                new Claim("Lastname", user.Lastname!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                //new Claim(ClaimTypes.Role, user.Rol)
            };

            // CREO EL PAYLOAD
            JwtPayload _Payload = new JwtPayload(
                    issuer: _configuration["JWT:ISSUER"],
                    audience: _configuration["JWT:AUDIENCE"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 24 horas.
                    expires: DateTime.UtcNow.AddHours(24)
                );

            // GENERO EL TOKEN
            JwtSecurityToken _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
        #endregion
    }
}

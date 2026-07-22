using Microsoft.IdentityModel.Tokens;
using MyTestMvc.Models.Setup;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Web.API.Models;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(HREmployee employee)
        {
              var claims = new List<Claim>
               {
                   new Claim(JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                   new Claim(ClaimTypes.Name, employee.Username),
                   new Claim(ClaimTypes.Email, employee.Email ?? string.Empty),
                   new Claim("IDHREmployee", employee.Id.ToString()),
                   new Claim("IdBranch", employee.IdHRBranch.ToString()),
                   new Claim("IdRole", employee.IdHRRole.ToString())
               };

            // Optional claims
            // claims.Add(new Claim(ClaimTypes.Role, employee.RoleName));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,

                ValidateLifetime = false, // Ignore expiration here

                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }

            return principal;
        }
    }
}
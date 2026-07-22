using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models.Entities;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public RefreshTokenService(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<RefreshToken> CreateToken(long idEmployee)
        {
            // Revoke existing active refresh tokens
            var activeTokens = await _context.RefreshToken
                .Where(x => x.IDHREmployee == idEmployee &&
                            !x.IsRevoked &&
                            x.Expires > DateTime.Now)
                .ToListAsync();

            foreach (var token in activeTokens)
            {
                token.IsRevoked = true;
                token.Revoked = DateTime.Now;
            }

            // Create a new refresh token
            var refreshToken = new RefreshToken
            {
                IDHREmployee = idEmployee,
                Token = _jwtService.GenerateRefreshToken(),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                IsRevoked = false
            };

            _context.RefreshToken.Add(refreshToken);

            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<RefreshToken?> GetToken(string token)
        {
            // FIX: Changed .Include to the virtual Navigation Property 'Employee' instead of long 'IDHREmployee'
            return await _context.RefreshToken
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task<bool> RevokeToken(string token)
        {
            var refreshToken = await GetToken(token);

            if (refreshToken == null)
                return false;

            if (refreshToken.IsRevoked)
                return false;

            refreshToken.IsRevoked = true;
            refreshToken.Revoked = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<RefreshToken> RotateToken(string token)
        {
            var refreshToken = await GetToken(token);

            if (refreshToken == null)
                throw new Exception("Invalid refresh token.");

            refreshToken.IsRevoked = true;
            refreshToken.Revoked = DateTime.Now;

            var newRefreshToken = new RefreshToken
            {
                IDHREmployee = refreshToken.IDHREmployee,
                Token = _jwtService.GenerateRefreshToken(),
                Created = DateTime.Now,
                Expires = refreshToken.Expires,
                IsRevoked = false
            };

            _context.RefreshToken.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return newRefreshToken;
        }
    }
}

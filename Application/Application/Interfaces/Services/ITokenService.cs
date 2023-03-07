using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Interfaces.Services
{
    public interface ITokenService
    {
        public JwtSecurityToken GenerateTokenSettings(SigningCredentials signingCredentials, List<Claim> claims);

        public Task<List<Claim>> GetClaims(Account user);

        public SigningCredentials GetSigningCredentials();

        public string GenerateRefreshToken();

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
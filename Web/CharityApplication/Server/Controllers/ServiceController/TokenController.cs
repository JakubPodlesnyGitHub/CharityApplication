using Application.Dtos.ServiceDtos.Responses;
using Application.Interfaces.Services;
using CharityApplication.Shared.Dtos.ServiceDtos.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CharityApplication.Server.Controllers.ServiceController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly ITokenService _tokenService;

        public TokenController(UserManager<Account> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> GetRefreshToken([FromBody] RefreshTokenDTO request)
        {
            if (request is null)
            {
                return BadRequest(new AuthResponseDTO { IsAuthSuccessful = false, ErrorMsg = "Invalid client request" });
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
            var userName = principal.Identity;
            var user = await _userManager.FindByIdAsync(principal.Claims.FirstOrDefault(x => x.Type.Contains("userId", StringComparison.OrdinalIgnoreCase))?.Value);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest(new AuthResponseDTO { IsAuthSuccessful = false, ErrorMsg = "Invalid client request" });
            }

            var signingCredentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(user);
            var tokenOptions = _tokenService.GenerateTokenSettings(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            user.RefreshToken = _tokenService.GenerateRefreshToken();
            await _userManager.UpdateAsync(user);

            return Ok(new AuthResponseDTO { IsAuthSuccessful = true, Token = token, RefreshToken = user.RefreshToken });
        }
    }
}
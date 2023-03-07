using Application.Dtos.ServiceDtos.Responses;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public static class ExternalAuthService
    {
        public static async Task<AuthResponseDTO> CheckGoogleCallback(AuthenticateResult result, UserManager<Account> userManager, SignInManager<Account> signInManager, ITokenService tokenService)
        {
            if (result.Succeeded && result.Principal != null)
            {
                var email = result.Principal.FindFirstValue(ClaimTypes.Email);
                var user = await userManager.FindByEmailAsync(email);
                if (user is null)
                {
                    user = new Account
                    {
                        Email = email,
                        UserName = email.Split("@", StringSplitOptions.None)[0],
                        PhoneNumber = result.Principal.FindFirstValue(ClaimTypes.MobilePhone) is not null ? result.Principal.FindFirstValue(ClaimTypes.MobilePhone) : result.Principal.FindFirstValue(ClaimTypes.HomePhone),
                        PrivateAccountNavigation = new PrivateAccount
                        {
                            FirstName = result.Principal.FindFirstValue(ClaimTypes.GivenName),
                            LastName = result.Principal.FindFirstValue(ClaimTypes.Surname),
                            BirthDate = result.Principal.FindFirstValue(ClaimTypes.DateOfBirth) is not null ? DateTime.Parse(result.Principal.FindFirstValue(ClaimTypes.DateOfBirth)) : null
                        }
                    };
                    var identityResult = await userManager.CreateAsync(user);
                    if (!identityResult.Succeeded)
                    {
                        throw new UnauthorizedAccessException("We were unable to create an account for you.");
                    }
                    await userManager.AddToRoleAsync(user, "PRIVATE_USER");
                }
                var externalLogin = new UserLoginInfo(GoogleDefaults.AuthenticationScheme,
                    result.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    result.Principal.FindFirst(ClaimTypes.Name)?.Value);
                var userExternalLogins = await userManager.GetLoginsAsync(user);
                if (!userExternalLogins.Any(x => x.ProviderKey.Equals(externalLogin.ProviderKey)))
                {
                    var addLoginResult = await userManager.AddLoginAsync(user, externalLogin);
                    if (!addLoginResult.Succeeded)
                    {
                        throw new UnauthorizedAccessException("We were unable to create an external login for you.");
                    }
                }
                await signInManager.SignInAsync(user, isPersistent: false);
                var responseDTO = await GenerateToken(userManager, tokenService, user);
                return responseDTO;
            }
            else
            {
                throw new UnauthorizedAccessException("Google Authentication failed - Chcek your credentials once more and try again");
            }
        }

        private static async Task<AuthResponseDTO> GenerateToken(UserManager<Account> userManager, ITokenService tokenService, Account user)
        {
            var credentials = tokenService.GetSigningCredentials();
            var claims = await tokenService.GetClaims(user);
            var tokenSettings = tokenService.GenerateTokenSettings(credentials, claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSettings);

            user.RefreshToken = tokenService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);

            await userManager.UpdateAsync(user);

            return new AuthResponseDTO { IsAuthSuccessful = true, Token = token, RefreshToken = user.RefreshToken };
        }
    }
}
using Application.Cqrs.Commands.Auth;
using Application.Dtos.ServiceDtos.Requests;
using Application.Dtos.ServiceDtos.Responses;
using Application.Interfaces.Services;
using Application.Providers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Cqrs.Handlers
{
    public class AuthHandler :
        IRequestHandler<UserLoginAuthCommand, AuthResponseDTO>,
        IRequestHandler<PrivateUserRegisterAuthCommand, AuthResponseDTO>,
        IRequestHandler<CompanyUserRegisterAuthCommand, AuthResponseDTO>
    {
        private readonly UserManager<Account> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AuthHandler(UserManager<Account> userManager, ITokenService tokenService, IMapper mapper, IEmailService emailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<AuthResponseDTO> Handle(UserLoginAuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid Authentication - Incorrect email or password");
            }
            var credentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(user);
            var tokenSettings = _tokenService.GenerateTokenSettings(credentials, claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSettings);

            user.RefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(5);

            await _userManager.UpdateAsync(user);

            return new AuthResponseDTO { IsAuthSuccessful = true, Token = token, RefreshToken = user.RefreshToken };
        }

        public async Task<AuthResponseDTO> Handle(PrivateUserRegisterAuthCommand request, CancellationToken cancellationToken)
        {
            var privateUser = _mapper.Map<Account>(request);
            IdentityResult result = await _userManager.CreateAsync(privateUser, request.Password);
            if (!result.Succeeded)
            {
                return new AuthResponseDTO { IsAuthSuccessful = false, ErrorMsg = "Registration doesn't succeded" };
            }

            await _userManager.AddToRoleAsync(privateUser, "PRIVATE_USER");

            var credentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(privateUser);
            var tokenSettings = _tokenService.GenerateTokenSettings(credentials, claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSettings);

            privateUser.RefreshToken = _tokenService.GenerateRefreshToken();
            privateUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);

            await _userManager.UpdateAsync(privateUser);

            await _emailService.SendEmailAsync(HTMLProvider.ProvideHtmlEmailTemaplate(
                new EmailRequestDTO
                {
                    FirstName = privateUser.PrivateAccountNavigation.FirstName,
                    LastName = privateUser.PrivateAccountNavigation.LastName,
                    To = privateUser.Email,
                    Subject = "Account Creation Confirmation"
                }, "AccountCreation"));
            return new AuthResponseDTO { IsAuthSuccessful = true, Token = token, RefreshToken = privateUser.RefreshToken };
        }

        public async Task<AuthResponseDTO> Handle(CompanyUserRegisterAuthCommand request, CancellationToken cancellationToken)
        {
            var companyUser = _mapper.Map<Account>(request);
            IdentityResult result = await _userManager.CreateAsync(companyUser, request.Password);
            if (!result.Succeeded)
            {
                return new AuthResponseDTO { IsAuthSuccessful = true, ErrorMsg = "Registration doesn't succeded" };
            }

            await _userManager.AddToRoleAsync(companyUser, "COMPANY");

            var credentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(companyUser);
            var tokenSettings = _tokenService.GenerateTokenSettings(credentials, claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSettings);

            companyUser.RefreshToken = _tokenService.GenerateRefreshToken();
            companyUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);

            await _userManager.UpdateAsync(companyUser);

            await _emailService.SendEmailAsync(HTMLProvider.ProvideHtmlEmailTemaplate(
               new EmailRequestDTO
               {
                   FirstName = companyUser.CompanyAccountNavigation.Name,
                   To = companyUser.Email,
                   Subject = "Account Creation Confirmation"
               }, "AccountCreation"));

            return new AuthResponseDTO { IsAuthSuccessful = true, Token = token, RefreshToken = companyUser.RefreshToken };
        }
    }
}
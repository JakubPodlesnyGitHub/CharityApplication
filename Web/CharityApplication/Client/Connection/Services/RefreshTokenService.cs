using Blazored.LocalStorage;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Connection.Interfaces.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Toolbelt.Blazor;

namespace CharityApplication.Client.Connection.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly AuthenticationStateProvider _authProvider;
        private readonly IAuthRepository _authRepository;
        private readonly ILocalStorageService _localStorageService;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        public RefreshTokenService(AuthenticationStateProvider authProvider, IAuthRepository authRepository, ILocalStorageService localStorageService)
        {
            _authProvider = authProvider;
            _authRepository = authRepository;
            _localStorageService = localStorageService;
        }

        public async Task<string> RefreshToken(HttpClientInterceptorEventArgs e)
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");
            var jwtToken = _jwtSecurityTokenHandler.ReadJwtToken(token);
            var expTime = jwtToken.ValidTo;
            var timeNow = DateTime.Now;
            var timeDiff = expTime - timeNow;
            if (timeDiff.TotalMinutes <= 1)
            {
                var refToken = await _authRepository.RefreshToken();
                return refToken;
            }
            return token;
        }
    }
}
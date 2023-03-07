using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CharityApplication.Client.Providers
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationState _anonymous;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
            {
                return _anonymous;
            }
            var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);
            if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
            {
                await _localStorageService.RemoveItemAsync("authToken");
                await _localStorageService.RemoveItemAsync("loggedUser");
                return _anonymous;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            JwtSecurityToken seciurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(seciurityToken.Claims.ToList(), "jwtAuthType")));
        }

        public async Task<int> NotifyUserAuthentication(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(_jwtSecurityTokenHandler.ReadJwtToken(token).Claims.ToList(), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            var userId = authenticatedUser.Claims.FirstOrDefault(x => x.Type.Contains("userId", StringComparison.OrdinalIgnoreCase))?.Value;
            NotifyAuthenticationStateChanged(authState);
            return int.Parse(userId);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
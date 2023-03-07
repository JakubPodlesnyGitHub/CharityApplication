using Application.Dtos.ServiceDtos.Responses;
using Blazored.LocalStorage;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.Auth;
using CharityApplication.Client.Providers;
using CharityApplication.Shared.Dtos.ServiceDtos.Requests;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;

namespace CharityApplication.Client.Connection.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHttpService _httpService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;
        private readonly IAccountRepository _accountReposiotry;
        private readonly string URL = "api/Auth";

        public AuthRepository(IHttpService httpService, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider, IAccountRepository accountRepository)
        {
            _httpService = httpService;
            _localStorageService = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
            _accountReposiotry = accountRepository;
        }

        public async Task<AuthResponseDTO> GoogleCallback()
        {
            //var response = await _httpService.Get<AuthResponseDTO>($"{URL}/OnGoogleGetCallback");
            //if (response.IfSucceeded)
            //{
            //    await _localStorageService.SetItemAsStringAsync("authToken", response.Response.Token);
            //    await _localStorageService.SetItemAsStringAsync("refreshToken", response.Response.RefreshToken);
            //    var userId = await ((AuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(response.Response.Token);
            //    await _localStorageService.SetItemAsync("loggedUser", JsonSerializer.Serialize(await _accountReposiotry.GetAccount(userId), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
            //}
            //return response.Response;
            return null;
        }

        public async Task<AuthResponseDTO> RegisterPrivateUser(PrivateAccountAuthModel privateAccount)
        {
            var response = await _httpService.Post<PrivateAccountAuthModel, AuthResponseDTO>($"{URL}/RegisterPrivateUser", privateAccount);
            if (response.IfSucceeded)
            {
                await _localStorageService.SetItemAsStringAsync("authToken", response.Response.Token);
                await _localStorageService.SetItemAsStringAsync("refreshToken", response.Response.RefreshToken);
                var userId = await ((AuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(response.Response.Token);
                await _localStorageService.SetItemAsync("loggedUser", 
                    JsonSerializer.Serialize(await _accountReposiotry.GetAccount(userId), 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
            }
            return response.Response;
        }

        public async Task<AuthResponseDTO> RegisterCompanyUser(CompanyAccountAuthModel companyAccount)
        {
            var response = await _httpService.Post<CompanyAccountAuthModel, AuthResponseDTO>($"{URL}/RegisterCompanyUser", companyAccount);
            if (response.IfSucceeded)
            {
                await _localStorageService.SetItemAsStringAsync("authToken", response.Response.Token);
                await _localStorageService.SetItemAsStringAsync("refreshToken", response.Response.RefreshToken);
                var userId = await ((AuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(response.Response.Token);
                await _localStorageService.SetItemAsync("loggedUser", JsonSerializer.Serialize(await _accountReposiotry.GetAccount(userId), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
            }
            return response.Response;
        }

        public async Task<AuthResponseDTO> LoginUser(LoginModel login)
        {
            var response = await _httpService.Post<LoginModel, AuthResponseDTO>($"{URL}/Login", login);
            if (response.IfSucceeded)
            {
                await _localStorageService.SetItemAsStringAsync("authToken", response.Response.Token);
                await _localStorageService.SetItemAsStringAsync("refreshToken", response.Response.RefreshToken);
                var userId = await ((AuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(response.Response.Token);
                await _localStorageService.SetItemAsync("loggedUser", JsonSerializer.Serialize(await _accountReposiotry.GetAccount(userId), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
            }
            return response.Response;
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            await _localStorageService.RemoveItemAsync("refreshToken");
            await _localStorageService.RemoveItemAsync("loggedUser");
            ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogout();
            _httpService.SetHttpClientAuthorization(null);
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorageService.GetItemAsync<string>("refreshToken");
            var tokenDTO =
                new RefreshTokenDTO
                {
                    Token = token,
                    RefreshToken = refreshToken
                };
            var refreshResult = await _httpService.Post<RefreshTokenDTO, AuthResponseDTO>("api/token/Refresh", tokenDTO);
            if (!refreshResult.IfSucceeded)
            {
                return refreshResult.Response.Detail;
            }
            if (!refreshResult.Response.IsAuthSuccessful || !refreshResult.IfSucceeded)
            {
                await Logout();
            }
            await _localStorageService.SetItemAsync("authToken", refreshResult.Response.Token);
            await _localStorageService.SetItemAsync("refreshToken", refreshResult.Response.RefreshToken);
            return refreshResult.Response.Token;
        }
    }
}
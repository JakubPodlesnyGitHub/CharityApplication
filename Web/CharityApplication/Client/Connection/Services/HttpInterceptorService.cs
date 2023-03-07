using Blazored.LocalStorage;
using CharityApplication.Client.Connection.Interfaces.Services;
using Toolbelt.Blazor;

namespace CharityApplication.Client.Connection.Services
{
    public class HttpInterceptorService
    {
        public readonly HttpClientInterceptor _interceptor;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ILocalStorageService _localStorageService;
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public HttpInterceptorService(HttpClientInterceptor interceptor, IRefreshTokenService refreshTokenService, ILocalStorageService localStorageService)
        {
            _interceptor = interceptor;
            _refreshTokenService = refreshTokenService;
            _localStorageService = localStorageService;
        }

        public void RegisterEvent()
        {
            _interceptor.BeforeSendAsync += InterceptBeforeSendAsync;
        }

        public async Task InterceptBeforeSendAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var path = e.Request.RequestUri.AbsolutePath;
            if (!path.Contains("token") && !path.Contains("auth"))
            {
                await _semaphore.WaitAsync();
                try
                {
                    if (await _localStorageService.ContainKeyAsync("authToken"))
                    {
                        var token = await _refreshTokenService.RefreshToken(e);
                        if (!string.IsNullOrEmpty(token))
                        {
                            e.Request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                        }
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        }

        public void DisposeEvent()
        {
            _interceptor.BeforeSendAsync -= InterceptBeforeSendAsync;
        }
    }
}
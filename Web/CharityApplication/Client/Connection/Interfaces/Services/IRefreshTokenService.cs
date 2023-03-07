using Toolbelt.Blazor;

namespace CharityApplication.Client.Connection.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        public Task<string> RefreshToken(HttpClientInterceptorEventArgs e);
    }
}
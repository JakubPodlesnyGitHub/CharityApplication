using System.Net.Http.Headers;

namespace CharityApplication.Client.Helpers.Http
{
    public interface IHttpService
    {
        public Task<HttpWrapper<T>> Get<T>(string URL);

        public Task<HttpWrapper<TResponse>> Post<T, TResponse>(string URL, T data);

        public Task<HttpWrapper<TResponse>> Put<T, TResponse>(string URL, T data);

        public Task<HttpWrapper<TResponse>> Delete<TResponse>(string URL);

        public void SetHttpClientAuthorization(AuthenticationHeaderValue authenticationHeaderValue);
    }
}
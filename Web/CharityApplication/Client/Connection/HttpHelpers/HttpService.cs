using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CharityApplication.Client.Helpers.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        private JsonSerializerOptions _jsonSerializerOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private string _mediaType = "application/json";

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpWrapper<T>> Get<T>(string URL)
        {
            var responseHttp = await _httpClient.GetAsync(_httpClient.BaseAddress + URL);

            var response = await Deserialize<T>(
                httpResponse: responseHttp,
                options: _jsonSerializerOptions);
            return new HttpWrapper<T>(responseHttp.IsSuccessStatusCode, response, responseHttp);
        }

        public async Task<HttpWrapper<TResponse>> Post<T, TResponse>(string URL, T data)
        {
            var transferredDataJSON = JsonSerializer.Serialize(data, _jsonSerializerOptions);
            var stringContent = new StringContent(
                content: transferredDataJSON,
                encoding: Encoding.UTF8,
                mediaType: _mediaType);
            var responseHttp = await _httpClient.PostAsync(requestUri: _httpClient.BaseAddress + URL, content: stringContent);
            var responseDeserialized = await Deserialize<TResponse>(responseHttp, _jsonSerializerOptions);
            return new HttpWrapper<TResponse>(responseHttp.IsSuccessStatusCode, responseDeserialized, responseHttp);
        }

        public async Task<HttpWrapper<TResponse>> Put<T, TResponse>(string URL, T data)
        {
            var transferredDataJSON = JsonSerializer.Serialize(data, _jsonSerializerOptions);
            var stringContent = new StringContent(
                content: transferredDataJSON,
                encoding: Encoding.UTF8,
                mediaType: _mediaType);
            var responseHttp = await _httpClient.PutAsync(requestUri: _httpClient.BaseAddress + URL, content: stringContent);
            var responseDeserialized = await Deserialize<TResponse>(responseHttp, _jsonSerializerOptions);
            return new HttpWrapper<TResponse>(responseHttp.IsSuccessStatusCode, responseDeserialized, responseHttp);
        }

        public async Task<HttpWrapper<TResponse>> Delete<TResponse>(string URL)
        {
            var responseHttp = await _httpClient.DeleteAsync(requestUri: _httpClient.BaseAddress + URL);
            var responseDeserialized = await Deserialize<TResponse>(responseHttp, _jsonSerializerOptions);
            return new HttpWrapper<TResponse>(responseHttp.IsSuccessStatusCode, responseDeserialized, responseHttp);
        }

        public void SetHttpClientAuthorization(AuthenticationHeaderValue authenticationHeaderValue)
        {
            _httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseString))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(responseString, options);
        }
    }
}
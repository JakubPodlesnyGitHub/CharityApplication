using CharityApplication.Client.Model.Error;

namespace CharityApplication.Client.Helpers.Http
{
    public class HttpWrapper<T>
    {
        public bool IfSucceeded { get; set; }
        public T Response { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }
        public ErrorResponseModel? ErrorResponse { get; set; }

        public HttpWrapper(bool ifSucceeded, T response, HttpResponseMessage httpResponseMessage, ErrorResponseModel errorResponse = null)
        {
            IfSucceeded = ifSucceeded;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
            ErrorResponse = errorResponse;
        }

        public async Task<string> GetBodyAsync()
        {
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
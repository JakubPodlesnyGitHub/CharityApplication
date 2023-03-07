using CharityApplication.Client.Connection.Interfaces.Services;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Shared.Dtos.ServiceDtos.Responses;

namespace CharityApplication.Client.Connection.Services
{
    public class QrCodeService : IQrCodeService
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/QrCode";

        public QrCodeService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<QrCodeDTO> CreateQrCode(string url)
        {
            var response = await _httpService.Post<string, QrCodeDTO>($"{URL}/CreateQrCode", url);
            return response.Response;
        }
    }
}
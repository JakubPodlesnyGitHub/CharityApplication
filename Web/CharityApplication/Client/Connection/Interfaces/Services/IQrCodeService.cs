using CharityApplication.Shared.Dtos.ServiceDtos.Responses;

namespace CharityApplication.Client.Connection.Interfaces.Services
{
    public interface IQrCodeService
    {
        public Task<QrCodeDTO> CreateQrCode(string url);
    }
}
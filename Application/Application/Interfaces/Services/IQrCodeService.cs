using CharityApplication.Shared.Dtos.ServiceDtos.Responses;

namespace Application.Interfaces.Services
{
    public interface IQrCodeService
    {
        public QrCodeDTO GetBase64QrCodeString(string URL);
    }
}
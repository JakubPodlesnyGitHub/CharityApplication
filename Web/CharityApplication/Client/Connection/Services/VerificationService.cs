using Application.Dtos.ServiceDtos.Requests;
using Application.Dtos.ServiceDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Services;
using CharityApplication.Client.Helpers.Http;

namespace CharityApplication.Client.Connection.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/Verification";

        public VerificationService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<VerifcationResponseDTO> VerifyAcccount(VerificationRequestDTO verificationRequest)
        {
            var response = await _httpService.Put<VerificationRequestDTO, VerifcationResponseDTO>($"{URL}/VerifyAcccount", verificationRequest);
            if (!response.IfSucceeded)
            {
                throw new ApplicationException(await response.GetBodyAsync());
            }
            return response.Response;
        }
    }
}
using Application.Dtos.ServiceDtos.Requests;
using Application.Dtos.ServiceDtos.Responses;

namespace CharityApplication.Client.Connection.Interfaces.Services
{
    public interface IVerificationService
    {
        public Task<VerifcationResponseDTO> VerifyAcccount(VerificationRequestDTO verificationRequest);
    }
}
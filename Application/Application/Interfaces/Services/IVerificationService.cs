using Application.Dtos.ServiceDtos.Requests;
using Application.Dtos.ServiceDtos.Responses;

namespace Application.Interfaces.Services
{
    public interface IVerificationService
    {
        public Task<VerifcationResponseDTO> VerifyAccount(VerificationRequestDTO request);
    }
}
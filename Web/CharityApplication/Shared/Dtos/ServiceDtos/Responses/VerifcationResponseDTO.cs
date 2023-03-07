using CharityApplication.Client.Model.Error;

namespace Application.Dtos.ServiceDtos.Responses
{
    public class VerifcationResponseDTO : ErrorResponseDTO
    {
        public bool IsVerificationSuccessful { get; set; }
        public string? ErrorMsg { get; set; }
        public string? Msg { get; set; }
    }
}
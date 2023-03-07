using CharityApplication.Client.Model.Error;

namespace Application.Dtos.ServiceDtos.Responses
{
    public class AuthResponseDTO : ErrorResponseDTO
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMsg { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
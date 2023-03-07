using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicCompanyRepresentativeDTO : ErrorResponseDTO
    {
        public int IdCompanyRepresentative { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string RepresentativeMail { get; set; } = null!;
        public string? RepresentativePhone { get; set; }
    }
}
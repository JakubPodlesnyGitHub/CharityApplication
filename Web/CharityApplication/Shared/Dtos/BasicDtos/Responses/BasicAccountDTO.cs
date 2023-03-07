using CharityApplication.Client.Model.Error;
using System.Text.Json.Serialization;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicAccountDTO : ErrorResponseDTO
    {
        public int IdAccount { get; set; }
        public string Email { get; set; } = null!;
        public bool VerificationStatus { get; set; }
        public bool GoldSponsorBadge { get; set; }
        public string Phone { get; set; } = null!;
        public string? Base64dataPicture { get; set; }
        public int Points { get; set; }
        public BasicPrivateAccountDTO? PrivateAccount { get; set; }
        public BasicCompanyAccountDTO? CompanyAccount { get; set; }

        [JsonIgnore]
        public string AccountCredentials { get => PrivateAccount is not null ? $"{PrivateAccount?.FirstName[..1]} {PrivateAccount?.LastName[..1]}" : CompanyAccount?.Name[..1]; }
    }
}
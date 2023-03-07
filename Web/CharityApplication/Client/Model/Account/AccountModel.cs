using Application.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Model.Account
{
    public class AccountModel
    {
        public int IdAccount { get; set; }
        public string Email { get; set; } = null!;
        public bool VerificationStatus { get; set; }
        public bool GoldSponsorBadge { get; set; }
        public string Phone { get; set; } = null!;
        public string? Base64dataPicture { get; set; }
        public BasicPrivateAccountDTO? PrivateAccount { get; set; }
        public BasicCompanyAccountDTO? CompanyAccount { get; set; }
    }
}
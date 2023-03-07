using CharityApplication.Client.Model.CompanyAddress;
using CharityApplication.Client.Model.CompanyRepresentative;

namespace CharityApplication.Client.Model.Account
{
    public class CompanyAccountModel
    {
        public int IdAccount { get; set; }
        public bool VerificationStatus { get; set; }
        public bool GoldSponsorBadge { get; set; }
        public string? Base64dataPicture { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? CompanyDesc { get; set; }

        public string? Krs { get; set; }
        public string? Nip { get; set; }

        public string? BankAccount { get; set; }
        public string? CompanyWebsiteLink { get; set; }

        public CompanyAddressModel? CompanyAddress { get; set; } = new CompanyAddressModel();
        public CompanyRepresentativeModel? CompanyRepresentative { get; set; } = new CompanyRepresentativeModel();
    }
}
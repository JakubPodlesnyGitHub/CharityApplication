using CharityApplication.Client.Model.CompanyAddress;
using CharityApplication.Client.Model.CompanyRepresentative;

namespace CharityApplication.Client.Model.Auth
{
    public class CompanyAccountAuthModel
    {
        public string Name { get; set; } = null!;
        public CompanyAddressModel CompanyAddress { get; set; } = new CompanyAddressModel();
        public CompanyRepresentativeModel CompanyRepresentative { get; set; } = new CompanyRepresentativeModel();
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RepeatedPassword { get; set; } = null!;
    }
}
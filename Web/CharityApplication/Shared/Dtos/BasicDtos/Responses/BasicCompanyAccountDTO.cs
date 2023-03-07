using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicCompanyAccountDTO : ErrorResponseDTO
    {
        public string Name { get; set; } = null!;
        public string CompanyDesc { get; set; } = null!;

        public string? Krs { get; set; }
        public string? Nip { get; set; }
        public int IdCompanyAddress { get; set; }
        public int IdCompanyRepresentative { get; set; }

        public string? BankAccount { get; set; }
        public string? CompanyWebsiteLink { get; set; }
        public BasicCompanyAddressDTO? CompanyAddress { get; set; }
        public BasicCompanyRepresentativeDTO? CompanyRepresentative { get; set; }
    }
}
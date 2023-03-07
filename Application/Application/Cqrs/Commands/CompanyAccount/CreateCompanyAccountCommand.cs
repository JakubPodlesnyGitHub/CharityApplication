using Application.Cqrs.Commands.CompanyAddress;
using Application.Cqrs.Commands.CompanyRepresentative;
using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.CompanyAccount
{
    public class CreateCompanyAccountCommand : IRequest<BasicAccountDTO>
    {
        public bool VerificationStatus { get; set; }
        public bool GoldSponsorBadge { get; set; }
        public string? Base64dataPicture { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? CompanyDesc { get; set; }

        public string? Krs { get; set; }
        public string? Nip { get; set; }

        public CreateCompanyAddressCommand CompanyAddress { get; set; }
        public CreateCompanyRepresentativeCommand CompanyRepresentative { get; set; }

        public string? BankAccount { get; set; }
        public string? CompanyWebsiteLink { get; set; }

        public CreateCompanyAccountCommand(
            string name,
            string password,
            string? companyDesc,
            string? krs,
            string? nip,
            CreateCompanyAddressCommand companyAddress,
            CreateCompanyRepresentativeCommand companyRepresentative,
            string? bankAccount,
            string? companyWebsiteLink,
            string email,
            bool verificationStatus,
            bool goldSponsorBadge,
            string phone,
            string? base64dataPicture)
        {
            Name = name;
            Password = password;
            CompanyDesc = companyDesc;
            Krs = krs;
            Nip = nip;
            CompanyAddress = companyAddress;
            CompanyRepresentative = companyRepresentative;
            BankAccount = bankAccount;
            CompanyWebsiteLink = companyWebsiteLink;
            Email = email;
            VerificationStatus = verificationStatus;
            GoldSponsorBadge = goldSponsorBadge;
            Phone = phone;
            Base64dataPicture = base64dataPicture;
        }
    }
}
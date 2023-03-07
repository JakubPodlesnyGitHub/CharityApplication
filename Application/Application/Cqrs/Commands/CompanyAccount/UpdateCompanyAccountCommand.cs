using Application.Cqrs.Commands.CompanyAddress;
using Application.Cqrs.Commands.CompanyRepresentative;
using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.CompanyAccount
{
    public class UpdateCompanyAccountCommand : IRequest<BasicAccountDTO>
    {
        public int IdAccount { get; set; }
        //public bool VerificationStatus { get; set; }
        //public bool GoldSponsorBadge { get; set; }
        //public string? Base64dataPicture { get; set; }
        //public string Email { get; set; } = null!;
        //public string Phone { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? CompanyDesc { get; set; }

        public string? Krs { get; set; }
        public string? Nip { get; set; }

        public UpdateCompanyAddressCommand CompanyAddress { get; set; }
        public UpdateCompanyRepresentativeCommand CompanyRepresentative { get; set; }

        public string? BankAccount { get; set; }
        public string? CompanyWebsiteLink { get; set; }

        public UpdateCompanyAccountCommand(
            int idAccount,
            //bool verificationStatus,
            //bool goldSponsorBadge,
            //string? base64dataPicture,
            //string email,
            //string phone,
            string name,
            string? companyDesc,
            string? krs,
            string? nip,
            UpdateCompanyAddressCommand companyAddress,
            UpdateCompanyRepresentativeCommand companyRepresentative,
            string? bankAccount,
            string? companyWebsiteLink)
        {
            IdAccount = idAccount;
            //VerificationStatus = verificationStatus;
            //GoldSponsorBadge = goldSponsorBadge;
            //Base64dataPicture = base64dataPicture;
            //Email = email;
            //Phone = phone;
            Name = name;
            CompanyDesc = companyDesc;
            Krs = krs;
            Nip = nip;
            CompanyAddress = companyAddress;
            CompanyRepresentative = companyRepresentative;
            BankAccount = bankAccount;
            CompanyWebsiteLink = companyWebsiteLink;
        }
    }
}
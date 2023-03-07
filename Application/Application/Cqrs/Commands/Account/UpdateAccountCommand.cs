using Application.Cqrs.Commands.CompanyAccount;
using Application.Cqrs.Commands.PrivateAccount;
using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Account
{
    public class UpdateAccountCommand : IRequest<BasicAccountDTO>
    {
        public int IdAccount { get; set; }
        public string Email { get; set; } = null!;
        public bool VerificationStatus { get; set; }
        public bool GoldSponsorBadge { get; set; }
        public string Phone { get; set; } = null!;
        public string? Base64dataPicture { get; set; }
        public UpdatePrivateAccountCommand? PrivateAccount { get; set; }
        public UpdateCompanyAccountCommand? CompanyAccount { get; set; }

        public UpdateAccountCommand(
            int idAccount,
            string email,
            bool verificationStatus,
            bool goldSponsorBadge,
            string phone,
            string? base64dataPicture,
            UpdatePrivateAccountCommand? privateAccount,
            UpdateCompanyAccountCommand? companyAccount)
        {
            IdAccount = idAccount;
            Email = email;
            VerificationStatus = verificationStatus;
            GoldSponsorBadge = goldSponsorBadge;
            Phone = phone;
            Base64dataPicture = base64dataPicture;
            PrivateAccount = privateAccount;
            CompanyAccount = companyAccount;
        }
    }
}
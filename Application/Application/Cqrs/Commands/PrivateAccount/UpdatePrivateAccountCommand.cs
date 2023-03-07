using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.PrivateAccount
{
    public class UpdatePrivateAccountCommand : IRequest<BasicAccountDTO>
    {
        public int IdAccount { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool VerificationStatus { get; set; }
        public bool GoldSponsorBadge { get; set; }
        public string? Base64dataPicture { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public UpdatePrivateAccountCommand(
            int idAccount,
            string email,
            string phone,
            bool verificationStatus,
            bool goldSponsorBadge,
            string? base64dataPicture,
            string firstName,
            string lastName,
            DateTime birthDate)
        {
            IdAccount = idAccount;
            Email = email;
            Phone = phone;
            VerificationStatus = verificationStatus;
            GoldSponsorBadge = goldSponsorBadge;
            Base64dataPicture = base64dataPicture;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }
    }
}
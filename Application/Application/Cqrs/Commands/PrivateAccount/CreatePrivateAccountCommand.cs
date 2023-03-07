using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.PrivateAccount
{
    public class CreatePrivateAccountCommand : IRequest<BasicAccountDTO>
    {
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool VerificationStatus { get; set; }
        public bool GoldSponsorBadge { get; set; }
        public string? Base64dataPicture { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public CreatePrivateAccountCommand(
            string email,
            string phone,
            string password,
            bool verificationStatus,
            bool goldSponsorBadge,
            string? base64dataPicture,
            string firstName,
            string lastName,
            DateTime birthDate)
        {
            Email = email;
            Phone = phone;
            Password = password;
            VerificationStatus = verificationStatus;
            GoldSponsorBadge = goldSponsorBadge;
            Base64dataPicture = base64dataPicture;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }
    }
}
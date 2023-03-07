using Application.Dtos.ServiceDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Auth
{
    public sealed class PrivateUserRegisterAuthCommand : IRequest<AuthResponseDTO>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RepeatedPassword { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public PrivateUserRegisterAuthCommand(
            string firstName,
            string lastName,
            DateTime birthDate,
            string email,
            string password,
            string repeatedPassword,
            string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Email = email;
            Password = password;
            RepeatedPassword = repeatedPassword;
            Phone = phone;
        }
    }
}
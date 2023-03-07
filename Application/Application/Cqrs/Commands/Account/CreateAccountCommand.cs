using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Account
{
    public class CreateAccountCommand : IRequest<BasicAccountDTO>
    {
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;

        public CreateAccountCommand(string email, string password, string phone)
        {
            Email = email;
            Password = password;
            Phone = phone;
        }
    }
}
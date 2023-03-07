using Application.Dtos.ServiceDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Auth
{
    public class UserLoginAuthCommand : IRequest<AuthResponseDTO>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public UserLoginAuthCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
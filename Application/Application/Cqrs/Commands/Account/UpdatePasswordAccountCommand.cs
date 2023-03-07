using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Account
{
    public class UpdatePasswordAccountCommand : IRequest<BasicAccountDTO>
    {
        public string Email { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string RepeatedNewPassword { get; set; } = null!;

        public UpdatePasswordAccountCommand(string email, string newPassword, string repeatedNewPassword)
        {
            Email = email;
            NewPassword = newPassword;
            RepeatedNewPassword = repeatedNewPassword;
        }
    }
}
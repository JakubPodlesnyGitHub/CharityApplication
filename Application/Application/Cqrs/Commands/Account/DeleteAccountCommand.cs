using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Account
{
    public class DeleteAccountCommand : IRequest<BasicAccountDTO>
    {
        public int IdAccount { get; set; }

        public DeleteAccountCommand(int idAccount)
        {
            IdAccount = idAccount;
        }
    }
}
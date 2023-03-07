using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.PrivateAccount
{
    public class DeletePrivateAccountCommand : IRequest<BasicAccountDTO>
    {
        public int IdAccount { get; set; }

        public DeletePrivateAccountCommand(int idAccount)
        {
            IdAccount = idAccount;
        }
    }
}
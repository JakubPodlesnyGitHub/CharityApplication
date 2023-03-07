using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.GroupAccount
{
    public class DeleteGroupAccountCommand : IRequest<BasicGroupAccountDTO>
    {
        public int IdAccount { get; set; }
        public int IdGroup { get; set; }

        public DeleteGroupAccountCommand(int idAccount, int idGroup)
        {
            IdAccount = idAccount;
            IdGroup = idGroup;
        }
    }
}
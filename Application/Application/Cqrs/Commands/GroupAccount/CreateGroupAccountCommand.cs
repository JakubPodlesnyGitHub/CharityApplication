using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.GroupAccount
{
    public class CreateGroupAccountCommand : IRequest<BasicGroupAccountDTO>
    {
        public int IdGroup { get; set; }
        public int IdAccount { get; set; }

        public CreateGroupAccountCommand(int idGroup, int idAccount)
        {
            IdGroup = idGroup;
            IdAccount = idAccount;
        }
    }
}
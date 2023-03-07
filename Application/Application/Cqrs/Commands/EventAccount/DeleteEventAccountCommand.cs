using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.EventAccount
{
    public class DeleteEventAccountCommand : IRequest<BasicEventAccountDTO>
    {
        public int IdAccount { get; set; }
        public int IdEvent { get; set; }

        public DeleteEventAccountCommand(int idAccount, int idEvent)
        {
            IdAccount = idAccount;
            IdEvent = idEvent;
        }
    }
}
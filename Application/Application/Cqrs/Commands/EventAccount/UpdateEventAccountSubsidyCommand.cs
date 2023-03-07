using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.EventAccount
{
    public class UpdateEventAccountSubsidyCommand : IRequest<BasicEventAccountDTO>
    {
        public int IdAccount { get; set; }
        public int IdEvent { get; set; }

        public UpdateEventAccountSubsidyCommand(int idAccount, int idEvent)
        {
            IdAccount = idAccount;
            IdEvent = idEvent;
        }
    }
}
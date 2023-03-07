using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.EventAccount
{
    public class UpdateEventAccountCommand : IRequest<BasicEventAccountDTO>
    {
        public int IdAccount { get; set; }
        public int IdEvent { get; set; }
        public bool IfPartcipantPresent { get; set; }

        public UpdateEventAccountCommand(int idAccount, int idEvent, bool ifPartcipantPresent)
        {
            IdAccount = idAccount;
            IdEvent = idEvent;
            IfPartcipantPresent = ifPartcipantPresent;
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.EventAccount
{
    public class CreateEventAccountCommand : IRequest<BasicEventAccountDTO>
    {
        public int IdAccount { get; set; }
        public int IdEvent { get; set; }
        public bool? IfPartcipantPresent { get; set; }

        public CreateEventAccountCommand(int idAccount, int idEvent, bool? ifPartcipantPresent)
        {
            IdAccount = idAccount;
            IdEvent = idEvent;
            IfPartcipantPresent = ifPartcipantPresent;
        }
    }
}
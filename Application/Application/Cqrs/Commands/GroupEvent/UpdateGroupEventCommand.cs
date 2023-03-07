using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.GroupEvent
{
    public class UpdateGroupEventCommand : IRequest<BasicGroupEventDTO>
    {
        public int IdGroup { get; set; }
        public int IdEvent { get; set; }
        public bool IfParticipantEvent { get; set; }

        public UpdateGroupEventCommand(int idGroup, int idEvent, bool ifPartcipantEvent)
        {
            IdGroup = idGroup;
            IdEvent = idEvent;
            IfParticipantEvent = ifPartcipantEvent;
        }
    }
}
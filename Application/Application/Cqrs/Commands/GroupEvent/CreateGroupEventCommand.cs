using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.GroupEvent
{
    public class CreateGroupEventCommand : IRequest<BasicGroupEventDTO>
    {
        public int IdGroup { get; set; }
        public int IdEvent { get; set; }

        public CreateGroupEventCommand(int idGroup, int idEvent)
        {
            IdGroup = idGroup;
            IdEvent = idEvent;
        }
    }
}
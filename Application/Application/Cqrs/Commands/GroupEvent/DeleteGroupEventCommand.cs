using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.GroupEvent
{
    public class DeleteGroupEventCommand : IRequest<BasicGroupEventDTO>
    {
        public int IdGroup { get; set; }
        public int IdEvent { get; set; }

        public DeleteGroupEventCommand(int idGroup, int idEvent)
        {
            IdGroup = idGroup;
            IdEvent = idEvent;
        }
    }
}
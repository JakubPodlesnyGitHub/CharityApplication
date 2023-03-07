using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Event
{
    public class DeleteEventCommand : IRequest<BasicEventDTO>
    {
        public int Id { get; set; }

        public DeleteEventCommand(int id)
        {
            Id = id;
        }
    }
}
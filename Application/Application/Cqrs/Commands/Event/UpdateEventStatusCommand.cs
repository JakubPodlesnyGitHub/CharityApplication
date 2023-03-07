using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Event
{
    public class UpdateEventStatusCommand : IRequest<BasicEventDTO>
    {
        public int IdStatus { get; set; }
        public int IdEvent { get; set; }
    }
}
using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Event
{
    public class GetEventByIdQuery : IRequest<BasicEventDTO>
    {
        public int Id { get; set; }

        public GetEventByIdQuery(int id)
        {
            Id = id;
        }
    }
}
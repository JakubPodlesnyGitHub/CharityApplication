using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Event
{
    public class GetEventsQuery : IRequest<List<BasicEventDTO>>
    {
    }
}
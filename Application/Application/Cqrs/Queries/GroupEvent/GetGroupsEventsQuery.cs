using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupEvent
{
    public class GetGroupsEventsQuery : IRequest<List<BasicGroupEventDTO>>
    {
    }
}
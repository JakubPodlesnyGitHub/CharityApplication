using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Badge
{
    public class GetBadgesQuery : IRequest<List<BasicBadgeDTO>>
    {
    }
}
using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Badge
{
    public class GetBadgeQueryId : IRequest<BasicBadgeDTO>
    {
        public int Id { get; set; }

        public GetBadgeQueryId(int id)
        {
            Id = id;
        }
    }
}
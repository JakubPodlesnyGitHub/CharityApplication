using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Status
{
    public class GetStatusesQuery : IRequest<List<BasicStatusDTO>>
    {
    }
}
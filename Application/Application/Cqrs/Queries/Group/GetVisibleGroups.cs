using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Group
{
    public class GetVisibleGroups : IRequest<List<BasicGroupDTO>>
    {
    }
}
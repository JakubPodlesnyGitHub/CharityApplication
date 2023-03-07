using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupName
{
    public class GetGroupNamesQuery : IRequest<List<BasicGroupNameDTO>>
    {
    }
}
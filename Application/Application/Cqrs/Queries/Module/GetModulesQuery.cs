using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Module
{
    public class GetModulesQuery : IRequest<List<BasicModuleDTO>>
    {
    }
}
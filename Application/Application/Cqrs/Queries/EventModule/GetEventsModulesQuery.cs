using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.EventModule
{
    public class GetEventsModulesQuery : IRequest<List<BasicEventModuleDTO>>
    {
    }
}
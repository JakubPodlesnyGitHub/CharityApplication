using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.EventModule
{
    public class GetEventModulesListByEventIdQuery : IRequest<List<BasicModuleDTO>>
    {
        public int IdEvent { get; set; }

        public GetEventModulesListByEventIdQuery(int idEvent)
        {
            IdEvent = idEvent;
        }
    }
}
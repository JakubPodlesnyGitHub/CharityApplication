using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.EventModule
{
    public class GetEventModuleByIdQuery : IRequest<BasicEventModuleDTO>
    {
        public int IdEventModule { get; set; }

        public GetEventModuleByIdQuery(int idEventModule)
        {
            IdEventModule = idEventModule;
        }
    }
}
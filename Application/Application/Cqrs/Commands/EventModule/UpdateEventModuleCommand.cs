using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.EventModule
{
    public class UpdateEventModuleCommand : IRequest<BasicEventModuleDTO>
    {
        public int IdEventModule { get; set; }
        public int IdModule { get; set; }
        public int IdEvent { get; set; }

        public UpdateEventModuleCommand(int idEventModule, int idModule, int idEvent)
        {
            IdEventModule = idEventModule;
            IdModule = idModule;
            IdEvent = idEvent;
        }
    }
}
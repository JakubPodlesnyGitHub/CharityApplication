using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.EventModule
{
    public class DeleteEventModuleCommand : IRequest<BasicEventModuleDTO>
    {
        public int IdEventModule { get; set; }

        public DeleteEventModuleCommand(int idEventModule)
        {
            IdEventModule = idEventModule;
        }
    }
}
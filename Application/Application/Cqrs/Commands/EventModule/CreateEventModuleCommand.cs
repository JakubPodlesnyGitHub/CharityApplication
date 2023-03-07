using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.EventModule
{
    public class CreateEventModuleCommand : IRequest<BasicEventModuleDTO>
    {
        public int IdModule { get; set; }
        public int IdEvent { get; set; }
    }
}
using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Module
{
    public class DeleteModuleCommand : IRequest<BasicModuleDTO>
    {
        public int IdModule { get; set; }

        public DeleteModuleCommand(int idModule)
        {
            IdModule = idModule;
        }
    }
}
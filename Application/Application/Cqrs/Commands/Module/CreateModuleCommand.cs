using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Module
{
    public class CreateModuleCommand : IRequest<BasicModuleDTO>
    {
        public string ModuleName { get; set; } = null!;
        public string ModuleDesc { get; set; } = null!;
        public string? Base64dataPicture { get; set; }

        public CreateModuleCommand(string moduleName, string moduleDesc, string? base64dataPicture)
        {
            ModuleName = moduleName;
            ModuleDesc = moduleDesc;
            Base64dataPicture = base64dataPicture;
        }
    }
}
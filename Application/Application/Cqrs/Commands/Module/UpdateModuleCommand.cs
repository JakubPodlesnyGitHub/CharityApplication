using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Module
{
    public class UpdateModuleCommand : IRequest<BasicModuleDTO>
    {
        public int IdModule { get; set; }
        public string ModuleName { get; set; } = null!;
        public string ModuleDesc { get; set; } = null!;
        public string? Base64dataPicture { get; set; }

        public UpdateModuleCommand(int idModule, string moduleName, string moduleDesc, string? base64dataPicture)
        {
            IdModule = idModule;
            ModuleName = moduleName;
            ModuleDesc = moduleDesc;
            Base64dataPicture = base64dataPicture;
        }
    }
}
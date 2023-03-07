using Application.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IModuleRepository
    {
        public Task<List<BasicModuleDTO>> GetModules();

        public Task<BasicModuleDTO> GetModule(int moduleId);
    }
}
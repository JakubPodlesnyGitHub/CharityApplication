using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;

namespace CharityApplication.Client.Connection.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/Module";

        public ModuleRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicModuleDTO>> GetModules()
        {
            var response = await _httpService.Get<List<BasicModuleDTO>>($"{URL}/GetModules");
            return response.Response;
        }

        public async Task<BasicModuleDTO> GetModule(int idModule)
        {
            var response = await _httpService.Get<BasicModuleDTO>($"{URL}/module/{idModule}");
            return response.Response;
        }
    }
}
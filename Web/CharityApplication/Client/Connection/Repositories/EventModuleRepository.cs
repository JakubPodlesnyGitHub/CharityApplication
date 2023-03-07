using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.EventModule;

namespace CharityApplication.Client.Connection.Repositories
{
    public class EventModuleRepository : IEventModuleRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/EventModule";

        public EventModuleRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicModuleDTO>> GetEventModules(int eventId)
        {
            var response = await _httpService.Get<List<BasicModuleDTO>>($"{URL}/eventModules/{eventId}");
            return response.Response;
        }

        public async Task<BasicEventModuleDTO> CreateEventModule(EventModuleModel eventModule)
        {
            var response = await _httpService.Post<EventModuleModel, BasicEventModuleDTO>($"{URL}/CreateEventModule", eventModule);
            return response.Response;
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Model.EventModule;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IEventModuleRepository
    {
        public Task<List<BasicModuleDTO>> GetEventModules(int eventId);

        public Task<BasicEventModuleDTO> CreateEventModule(EventModuleModel eventModule);
    }
}
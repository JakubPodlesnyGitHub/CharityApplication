using Application.Dtos.BasicDtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IEventModuleRepository : IBaseRepository<EventModule>
    {
        public Task<List<BasicModuleDTO>> GetEventModulesListByEventId(int idEvent);
    }
}
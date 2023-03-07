using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IGroupEventRepository : IBaseRepository<GroupEvent>
    {
        public Task<List<GroupEvent>> GetEventGroupsByEventId(int eventId);
    }
}
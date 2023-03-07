using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        public Task<List<Event>> GetRelatedEventsByAccountIdAsync(int accountId);

        public Task<List<Event>> GetRelatedEventsByGroupIdAsync(int groupId);
    }
}
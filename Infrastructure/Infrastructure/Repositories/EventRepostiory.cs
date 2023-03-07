using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepostiory : BaseRepository<Event>, IEventRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;

        public EventRepostiory(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
        }

        public async Task<List<Event>> GetRelatedEventsByAccountIdAsync(int accountId)
        {
            return await _charityApplicationDbContext.Events.Where(e => e.AccountEvents.Any(ae => ae.IdAccount == accountId)).ToListAsync();
        }

        public async Task<List<Event>> GetRelatedEventsByGroupIdAsync(int groupId)
        {
            return await _charityApplicationDbContext.Events
                .Include(e => e.GroupEvents)
                .Where(e => e.GroupEvents.Any(ge => ge.IdGroup == groupId))
                .ToListAsync();
        }
    }
}
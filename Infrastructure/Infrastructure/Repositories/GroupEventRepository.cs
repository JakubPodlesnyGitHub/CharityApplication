using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GroupEventRepository : BaseRepository<GroupEvent>, IGroupEventRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;

        public GroupEventRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
        }

        public async Task<List<GroupEvent>> GetEventGroupsByEventId(int eventId)
        {
            return await _charityApplicationDbContext.GroupEvents.Where(ge => ge.IdEvent == eventId).ToListAsync();
        }
    }
}
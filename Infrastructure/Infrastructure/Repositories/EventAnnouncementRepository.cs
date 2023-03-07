using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventAnnouncementRepository : BaseRepository<EventAnnouncement>, IEventAnnouncementRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;

        public EventAnnouncementRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
        }

        public async Task<List<EventAnnouncement>> GetEventAnnouncementsByEventId(int eventId)
        {
            return await _charityApplicationDbContext.EventAnnouncements.Where(e => e.IdEvent == eventId).ToListAsync();
        }
    }
}
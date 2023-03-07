using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IEventAnnouncementRepository : IBaseRepository<EventAnnouncement>
    {
        public Task<List<EventAnnouncement>> GetEventAnnouncementsByEventId(int eventId);
    }
}
using CharityApplication.Client.Model.EventAnnouncement;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IEventAnnouncementRepository
    {
        public Task<BasicEventAnnouncementDTO> CreateEventAnnouncement(EventAnnouncementModel eventAnnouncement);

        public Task<List<BasicEventAnnouncementDTO>> GetEventAnnouncements(int eventId);

        public Task<BasicEventAnnouncementDTO> GetEventAnnouncement(int eventannouncementId);

        public Task<BasicEventAnnouncementDTO> DeleteEventAnnouncement(int eventannouncementId);

        public Task<BasicEventAnnouncementDTO> UpdateEventAnnouncement(EventAnnouncementModel eventAnnouncement);
    }
}
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.EventAnnouncement;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Connection.Repositories
{
    public class EventAnnouncementRepository : IEventAnnouncementRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/EventAnnouncement";

        public EventAnnouncementRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicEventAnnouncementDTO>> GetEventAnnouncements(int eventId)
        {
            var response = await _httpService.Get<List<BasicEventAnnouncementDTO>>($"{URL}/eventannouncements-by-event/{eventId}");
            return response.Response;
        }

        public async Task<BasicEventAnnouncementDTO> GetEventAnnouncement(int eventannouncementId)
        {
            var response = await _httpService.Get<BasicEventAnnouncementDTO>($"{URL}/eventannouncement/{eventannouncementId}");
            return response.Response;
        }

        public async Task<BasicEventAnnouncementDTO> CreateEventAnnouncement(EventAnnouncementModel eventAnnouncement)
        {
            var response = await _httpService.Post<EventAnnouncementModel, BasicEventAnnouncementDTO>($"{URL}/CreateEventAnnouncement", eventAnnouncement);
            return response.Response;
        }

        public async Task<BasicEventAnnouncementDTO> DeleteEventAnnouncement(int eventannouncementId)
        {
            var response = await _httpService.Delete<BasicEventAnnouncementDTO>($"{URL}/eventannouncement/{eventannouncementId}");
            return response.Response;
        }

        public async Task<BasicEventAnnouncementDTO> UpdateEventAnnouncement(EventAnnouncementModel eventAnnouncement)
        {
            var response = await _httpService.Put<EventAnnouncementModel, BasicEventAnnouncementDTO>($"{URL}/UpdateEventAnnouncement", eventAnnouncement);
            return response.Response;
        }
    }
}
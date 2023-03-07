using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.EventModel;

namespace CharityApplication.Client.Connection.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/Event";

        public EventRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicEventDTO>> GetEventsList()
        {
            var response = await _httpService.Get<List<BasicEventDTO>>($"{URL}/GetEvents");
            return response.Response;
        }

        public async Task<BasicEventDTO> GetEvent(int? idEvent)
        {
            var response = await _httpService.Get<BasicEventDTO>($"{URL}/event/{idEvent}");
            return response.Response;
        }

        public async Task<List<BasicEventDTO>> GetEventsByGroupId(int groupId)
        {
            var response = await _httpService.Get<List<BasicEventDTO>>($"{URL}/events-by-group/{groupId}");
            return response.Response;
        }

        public async Task<List<BasicEventDTO>> GetEventsByAccountId(int accountId)
        {
            var response = await _httpService.Get<List<BasicEventDTO>>($"{URL}/events-by-account/{accountId}");
            return response.Response;
        }

        public async Task<BasicEventDTO> CreateEvent(EventModel eventModel)
        {
            var response = await _httpService.Post<EventModel, BasicEventDTO>($"{URL}/CreateEvent", eventModel);
            return response.Response;
        }

        public async Task<BasicEventDTO> UpdateEvent(EventModel eventModel)
        {
            var response = await _httpService.Put<EventModel, BasicEventDTO>($"{URL}/UpdateEvent", eventModel);
            return response.Response;
        }

        public async Task<BasicEventDTO> UpdateEvent(BasicEventDTO eventModel)
        {
            var response = await _httpService.Put<BasicEventDTO, BasicEventDTO>($"{URL}/UpdateEvent", eventModel);
            return response.Response;
        }

        public async Task<BasicEventDTO> UpdateEventStatus(EventStatusModel eventStatus)
        {
            var response = await _httpService.Put<EventStatusModel, BasicEventDTO>($"{URL}/UpdateEventStatus", eventStatus);
            return response.Response;
        }

        public async Task<BasicEventDTO> DeleteEvent(int eventId)
        {
            var response = await _httpService.Delete<BasicEventDTO>($"{URL}/event/{eventId}");
            return response.Response;
        }
    }
}
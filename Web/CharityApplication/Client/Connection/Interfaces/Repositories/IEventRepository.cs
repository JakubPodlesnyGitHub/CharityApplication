using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Model.EventModel;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IEventRepository
    {
        public Task<List<BasicEventDTO>> GetEventsList();

        public Task<BasicEventDTO> GetEvent(int? idEvent);

        public Task<List<BasicEventDTO>> GetEventsByGroupId(int groupId);

        public Task<List<BasicEventDTO>> GetEventsByAccountId(int accountId);

        public Task<BasicEventDTO> CreateEvent(EventModel eventModel);

        public Task<BasicEventDTO> UpdateEvent(EventModel eventModel);

        public Task<BasicEventDTO> UpdateEvent(BasicEventDTO eventModel);

        public Task<BasicEventDTO> UpdateEventStatus(EventStatusModel eventStatus);

        public Task<BasicEventDTO> DeleteEvent(int eventId);
    }
}
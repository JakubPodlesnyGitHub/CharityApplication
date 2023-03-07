using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.AccountEvent;

namespace CharityApplication.Client.Connection.Repositories
{
    public class EventAccountRepository : IEventAccountRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/EventAccount";

        public EventAccountRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<BasicEventAccountDTO> CreateEventAccount(EventAccountModel eventAccount)
        {
            var response = await _httpService.Post<EventAccountModel, BasicEventAccountDTO>($"{URL}/CreateEventAccount", eventAccount);
            return response.Response;
        }

        public async Task<BasicEventAccountDTO> UpdateEventAccount(EventAccountModel eventAccount)
        {
            var response = await _httpService.Put<EventAccountModel, BasicEventAccountDTO>($"{URL}/UpdateEventAccount", eventAccount);
            return response.Response;
        }

        public async Task<BasicEventAccountDTO> DeleteEventAccount(int eventId, int accountId)
        {
            var response = await _httpService.Delete<BasicEventAccountDTO>($"{URL}/DeleteEventAccount?eventId={eventId}&accountId={accountId}");
            return response.Response;
        }

        public async Task<BasicEventAccountDTO> UpdateEventAccountSubsidy(EventAccountModel eventAccount)
        {
            var response = await _httpService.Put<EventAccountModel, BasicEventAccountDTO>($"{URL}/UpdateEventAccountSubsidy", eventAccount);
            return response.Response;
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;

namespace CharityApplication.Client.Connection.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/Status";

        public StatusRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicStatusDTO>> GetEventStatuses()
        {
            var response = await _httpService.Get<List<BasicStatusDTO>>($"{URL}/GetEventStatutes");
            return response.Response;
        }
    }
}
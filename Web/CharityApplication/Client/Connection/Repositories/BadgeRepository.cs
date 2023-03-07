using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;

namespace CharityApplication.Client.Connection.Repositories
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/Badge";

        public BadgeRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicBadgeDTO>> GetBadges()
        {
            var response = await _httpService.Get<List<BasicBadgeDTO>>($"{URL}/GetBadges");
            return response.Response;
        }
    }
}
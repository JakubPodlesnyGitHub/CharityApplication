using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;

namespace CharityApplication.Client.Connection.Repositories
{
    public class BadgeAccountRepository : IBadgeAccountRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/BadgeAccount";

        public BadgeAccountRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicBadgeAccountDTO>> GetAccountBadgesById(int accountId)
        {
            var response = await _httpService.Get<List<BasicBadgeAccountDTO>>($"{URL}/accountbadges/{accountId}");
            return response.Response;
        }
    }
}
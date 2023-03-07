using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;

namespace CharityApplication.Client.Connection.Repositories
{
    public class BadgeGroupRepository : IBadgeGroupRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/BadgeGroup";

        public BadgeGroupRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicBadgeGroupDTO>> GetGroupBadges(int groupId)
        {
            var response = await _httpService.Get<List<BasicBadgeGroupDTO>>($"{URL}/groupbadges/{groupId}");
            return response.Response;
        }
    }
}
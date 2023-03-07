using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;

namespace CharityApplication.Client.Connection.Repositories
{
    public class GroupNameRepository : IGroupNameRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/GroupName";

        public GroupNameRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicGroupNameDTO>> GetGroupNames()
        {
            var response = await _httpService.Get<List<BasicGroupNameDTO>>($"{URL}/GetGroupNames");
            return response.Response;
        }
    }
}
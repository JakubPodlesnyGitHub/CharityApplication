using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.Group;

namespace CharityApplication.Client.Connection.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/Group";
        private readonly int NUMBER_OF_TOP_RECORDS = 10;

        public GroupRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicGroupDTO>> GetPublicGroups()
        {
            var response = await _httpService.Get<List<BasicGroupDTO>>($"{URL}/GetVisibleGroups");
            return response.Response;
        }

        public async Task<List<BasicGroupDTO>> GetPublicPrivateGroups(int accountId)
        {
            var response = await _httpService.Get<List<BasicGroupDTO>>($"{URL}/public-private-groups/{accountId}");
            return response.Response;
        }

        public async Task<BasicGroupDTO> GetGroup(int idGroup)
        {
            var response = await _httpService.Get<BasicGroupDTO>($"{URL}/group/{idGroup}");
            return response.Response;
        }

        public async Task<List<BasicGroupDTO>> GetGroupsByEventId(int eventId)
        {
            var response = await _httpService.Get<List<BasicGroupDTO>>($"{URL}/groups-by-event/{eventId}");
            return response.Response;
        }

        public async Task<List<BasicGroupDTO>> GetGroupsByAccountId(int accountId)
        {
            var response = await _httpService.Get<List<BasicGroupDTO>>($"{URL}/groups-by-account/{accountId}");
            return response.Response;
        }

        public async Task<List<GroupWithBadgePointsDTO>> GetTopGroupsWithMostPoints()
        {
            var response = await _httpService.Get<List<GroupWithBadgePointsDTO>>($"{URL}/topgroups/{NUMBER_OF_TOP_RECORDS}");
            return response.Response;
        }

        public async Task<BasicGroupDTO> CreateGroup(GroupModel group)
        {
            var response = await _httpService.Post<GroupModel, BasicGroupDTO>($"{URL}/CreateGroup", group);
            return response.Response;
        }

        public async Task<BasicGroupDTO> UpdateGroup(GroupModel group)
        {
            var response = await _httpService.Put<GroupModel, BasicGroupDTO>($"{URL}/UpdateGroup", group);
            return response.Response;
        }

        public async Task<BasicGroupDTO> DeleteGroup(int groupId)
        {
            var response = await _httpService.Delete<BasicGroupDTO>($"{URL}/group/{groupId}");
            return response.Response;
        }
    }
}
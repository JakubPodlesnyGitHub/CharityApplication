using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.AccountGroup;

namespace CharityApplication.Client.Connection.Repositories
{
    public class GroupAccoutRepository : IGroupAccountRepository
    {
        private readonly IHttpService _httpService;
        private string URL = "api/GroupAccount";

        public GroupAccoutRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<BasicGroupAccountDTO> CreateGroupAccount(GroupAccountModel groupAccount)
        {
            var response = await _httpService.Post<GroupAccountModel, BasicGroupAccountDTO>($"{URL}/CreateGroupAccount", groupAccount);
            return response.Response;
        }

        public async Task<BasicGroupAccountDTO> DeleteGroupAccount(int accountId, int groupId)
        {
            var response = await _httpService.Delete<BasicGroupAccountDTO>($"{URL}/DeleteGroupAccount?accountId={accountId}&groupId={groupId}");
            return response.Response;
        }
    }
}
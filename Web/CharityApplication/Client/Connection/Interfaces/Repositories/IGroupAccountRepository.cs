using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Model.AccountGroup;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IGroupAccountRepository
    {
        public Task<BasicGroupAccountDTO> CreateGroupAccount(GroupAccountModel groupAccount);

        public Task<BasicGroupAccountDTO> DeleteGroupAccount(int accountId, int groupId);
    }
}
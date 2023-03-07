using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IGroupAccountRepository : IBaseRepository<GroupAccount>
    {
        public Task<List<GroupAccount>> GetGroupAccountsByGroupId(int groupId);

        public Task<List<GroupAccount>> GetPartcipantOfEventGroupMembers(int groupId, int eventId);
    }
}
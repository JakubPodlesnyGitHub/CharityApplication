using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IBadgeRepository : IBaseRepository<Badge>
    {
        public Task<List<Badge>> GetBadgesByAccountId(int accountId);

        public Task<List<Badge>> GetBadgesByGroupId(int groupId);
    }
}
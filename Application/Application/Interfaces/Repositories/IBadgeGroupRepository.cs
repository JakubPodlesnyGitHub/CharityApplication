using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IBadgeGroupRepository : IBaseRepository<BadgeGroup>
    {
        public Task<List<BadgeGroup>> GetGroupBadgesByGroupId(int groupId);
    }
}
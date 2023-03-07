using Application.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IBadgeGroupRepository
    {
        public Task<List<BasicBadgeGroupDTO>> GetGroupBadges(int groupId);
    }
}
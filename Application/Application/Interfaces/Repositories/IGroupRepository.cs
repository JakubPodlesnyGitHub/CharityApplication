using Application.Dtos.ExtendedDtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        public Task<List<GroupWithBadgePointsDTO>> GetTopgGroupsWithBadgePoints(int numberOfGroups);

        public Task<List<Group>> GetPublicGroups();

        public Task<List<Group>> GetPublicPrivateGroups(int accountId);

        public Task<List<Group>> GetGroupsByAccountId(int accountId);

        public Task<List<Group>> GetGroupsByEventId(int eventId);
    }
}
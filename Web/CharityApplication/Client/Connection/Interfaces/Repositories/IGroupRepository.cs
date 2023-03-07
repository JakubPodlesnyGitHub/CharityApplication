using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using CharityApplication.Client.Model.Group;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        public Task<List<BasicGroupDTO>> GetPublicGroups();

        public Task<List<BasicGroupDTO>> GetPublicPrivateGroups(int accountId);

        public Task<BasicGroupDTO> GetGroup(int idGroup);

        public Task<List<GroupWithBadgePointsDTO>> GetTopGroupsWithMostPoints();

        public Task<List<BasicGroupDTO>> GetGroupsByAccountId(int accountId);

        public Task<List<BasicGroupDTO>> GetGroupsByEventId(int eventId);

        public Task<BasicGroupDTO> CreateGroup(GroupModel group);

        public Task<BasicGroupDTO> UpdateGroup(GroupModel group);

        public Task<BasicGroupDTO> DeleteGroup(int groupId);
    }
}
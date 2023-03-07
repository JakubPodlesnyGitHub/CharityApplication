using CharityApplication.Client.Model.GroupAnnouncement;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IGroupAnnouncementRepository
    {
        public Task<BasicGroupAnnouncementDTO> CreateGroupAnnouncement(GroupAnnouncementModel groupAnnouncement);

        public Task<List<BasicGroupAnnouncementDTO>> GetGroupAnnouncements(int groupId);

        public Task<BasicGroupAnnouncementDTO> GetGroupAnnouncement(int groupannouncementId);

        public Task<BasicGroupAnnouncementDTO> DeleteGroupAnnouncement(int groupannouncementId);

        public Task<BasicGroupAnnouncementDTO> UpdateGroupAnnouncement(GroupAnnouncementModel groupAnnouncement);
    }
}
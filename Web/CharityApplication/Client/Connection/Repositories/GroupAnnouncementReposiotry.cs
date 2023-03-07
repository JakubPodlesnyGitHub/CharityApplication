using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.GroupAnnouncement;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Connection.Repositories
{
    public class GroupAnnouncementReposiotry : IGroupAnnouncementRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/GroupAnnouncement";

        public GroupAnnouncementReposiotry(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicGroupAnnouncementDTO>> GetGroupAnnouncements(int groupId)
        {
            var response = await _httpService.Get<List<BasicGroupAnnouncementDTO>>($"{URL}/groupannouncements-by-group/{groupId}");
            return response.Response;
        }

        public async Task<BasicGroupAnnouncementDTO> GetGroupAnnouncement(int groupannouncementId)
        {
            var response = await _httpService.Get<BasicGroupAnnouncementDTO>($"{URL}/groupannouncement/{groupannouncementId}");
            return response.Response;
        }

        public async Task<BasicGroupAnnouncementDTO> CreateGroupAnnouncement(GroupAnnouncementModel groupAnnouncement)
        {
            var response = await _httpService.Post<GroupAnnouncementModel, BasicGroupAnnouncementDTO>($"{URL}/CreateGroupAnnouncement", groupAnnouncement);
            return response.Response;
        }

        public async Task<BasicGroupAnnouncementDTO> DeleteGroupAnnouncement(int groupannouncementId)
        {
            var response = await _httpService.Delete<BasicGroupAnnouncementDTO>($"{URL}/groupannouncement/{groupannouncementId}");
            return response.Response;
        }

        public async Task<BasicGroupAnnouncementDTO> UpdateGroupAnnouncement(GroupAnnouncementModel groupAnnouncement)
        {
            var response = await _httpService.Put<GroupAnnouncementModel, BasicGroupAnnouncementDTO>($"{URL}/UpdateGroupAnnouncement", groupAnnouncement);
            return response.Response;
        }
    }
}
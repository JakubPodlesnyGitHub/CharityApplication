using CharityApplication.Client.Model.Error;

namespace CharityApplication.Shared.Dtos.BasicDtos.Responses
{
    public class BasicEventAnnouncementDTO : ErrorResponseDTO
    {
        public int IdEventAnnouncement { get; set; }
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int IdEvent { get; set; }
        public int IdOwner { get; set; }
    }
}
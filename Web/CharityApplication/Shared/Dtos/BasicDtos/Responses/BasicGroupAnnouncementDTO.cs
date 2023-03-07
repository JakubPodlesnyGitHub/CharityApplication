using CharityApplication.Client.Model.Error;

namespace CharityApplication.Shared.Dtos.BasicDtos.Responses
{
    public class BasicGroupAnnouncementDTO : ErrorResponseDTO
    {
        public int IdGroupAnnouncement { get; set; }
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int IdGroup { get; set; }
        public int IdOwner { get; set; }
    }
}
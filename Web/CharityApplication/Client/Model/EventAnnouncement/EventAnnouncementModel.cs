namespace CharityApplication.Client.Model.EventAnnouncement
{
    public class EventAnnouncementModel
    {
        public int? IdEventAnnouncement { get; set; }
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int IdEvent { get; set; }
        public int IdOwner { get; set; }
    }
}
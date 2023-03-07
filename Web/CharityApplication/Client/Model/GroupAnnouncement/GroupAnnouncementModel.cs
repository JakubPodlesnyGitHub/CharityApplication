namespace CharityApplication.Client.Model.GroupAnnouncement
{
    public class GroupAnnouncementModel
    {
        public int? IdGroupAnnouncement { get; set; }
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int IdGroup { get; set; }
        public int IdOwner { get; set; }
    }
}
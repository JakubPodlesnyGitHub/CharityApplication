namespace Domain.Entities
{
    public class Group
    {
        public Group()
        {
            GroupAccounts = new HashSet<GroupAccount>();
            BadgeGroups = new HashSet<BadgeGroup>();
            GroupAnnouncements = new HashSet<GroupAnnouncement>();
        }

        public int IdGroup { get; set; }
        public int IdGroupName { get; set; }
        public int NumberOfParticipants { get; set; }
        public DateTime CreationGroupDate { get; set; }
        public string Description { get; set; } = null!;
        public bool GroupType { get; set; }
        public string? Base64dataPicture { get; set; }
        public int Points { get; set; }
        public int IdGroupOwner { get; set; }

        public virtual GroupName GroupNameNavigation { get; set; } = null!;
        public virtual ICollection<GroupAccount> GroupAccounts { get; set; }
        public virtual ICollection<GroupAnnouncement> GroupAnnouncements { get; set; }
        public virtual ICollection<BadgeGroup> BadgeGroups { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
    }
}
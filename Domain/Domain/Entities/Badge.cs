namespace Domain.Entities
{
    public class Badge
    {
        public Badge()
        {
            BadgeGroups = new HashSet<BadgeGroup>();
            BadgeAccounts = new HashSet<BadgeAccount>();
        }

        public int IdBadge { get; set; }
        public string Name { get; set; } = null!;
        public string BadgeGoal { get; set; }
        public int Pointstreshold_User { get; set; }
        public int Pointstreshold_Group { get; set; }
        public string? Base64dataPicture { get; set; }

        public virtual ICollection<BadgeGroup> BadgeGroups { get; set; }
        public virtual ICollection<BadgeAccount> BadgeAccounts { get; set; }
    }
}
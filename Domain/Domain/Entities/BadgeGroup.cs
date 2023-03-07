namespace Domain.Entities
{
    public class BadgeGroup
    {
        public int IdBadge { get; set; }
        public int IdGroup { get; set; }
        public DateTime DateOfReceivingTheBadge { get; set; }

        public virtual Group GroupNavigation { get; set; } = null!;
        public virtual Badge BadgeNavigation { get; set; } = null!;
    }
}
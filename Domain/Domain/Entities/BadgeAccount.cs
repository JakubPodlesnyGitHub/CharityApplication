namespace Domain.Entities
{
    public class BadgeAccount
    {
        public int IdBadge { get; set; }
        public int IdAccount { get; set; }
        public DateTime DateOfReceivingTheBadge { get; set; }

        public virtual Badge BadgeNavigation { get; set; }
        public virtual Account AccountNavigation { get; set; }
    }
}
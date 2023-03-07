namespace Domain.Entities
{
    public class AccountEvent
    {
        public int IdAccount { get; set; }
        public int IdEvent { get; set; }
        public bool IfPartcipantPresent { get; set; }
        public int SubsidyAmount { get; set; }
        public DateTime EventCreation { get; set; }

        public virtual Account AccountNavigation { get; set; } = null!;
        public virtual Event EventNavigation { get; set; } = null!;
    }
}
namespace Domain.Entities
{
    public class GroupEvent
    {
        public int IdGroup { get; set; }
        public int IdEvent { get; set; }
        public bool IfParticipantEvent { get; set; }
        public DateTime GroupEventCreation { get; set; }

        public virtual Group GroupNavigation { get; set; } = null!;
        public virtual Event EventNavigation { get; set; } = null!;
    }
}
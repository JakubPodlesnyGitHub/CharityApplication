namespace Domain.Entities
{
    public class Event
    {
        public int IdEvent { get; set; }
        public string EventName { get; set; } = null!;
        public string EventGoal { get; set; } = null!;
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public int EventMemeberLimit { get; set; }
        public decimal OverSale { get; set; }
        public string EventDesc { get; set; } = null!;
        public string? JsonEvent { get; set; }
        public int IdStatus { get; set; }
        public string? Base64dataPicture { get; set; }
        public int IdEventOwner { get; set; }

        public virtual Status StatusNavigation { get; set; }
        public virtual ICollection<AssesmentForm> AssesmentForms { get; set; }
        public virtual ICollection<AccountEvent> AccountEvents { get; set; }
        public virtual ICollection<EventAnnouncement> EventAnnouncements { get; set; }
        public virtual ICollection<EventModule> EventModules { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
    }
}
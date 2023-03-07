namespace Domain.Entities
{
    public class Status
    {
        public Status()
        {
            Events = new HashSet<Event>();
        }

        public int IdStatus { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
    }
}
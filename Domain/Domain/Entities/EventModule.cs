namespace Domain.Entities
{
    public class EventModule
    {
        public int IdEventModule { get; set; }
        public int IdModule { get; set; }
        public int IdEvent { get; set; }

        public virtual Module ModuleNavigation { get; set; } = null!;
        public virtual Event EventNavigation { get; set; } = null!;
    }
}
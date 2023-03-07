namespace Domain.Entities
{
    public class Module
    {
        public Module()
        {
            EventModules = new HashSet<EventModule>();
        }

        public int IdModule { get; set; }
        public string ModuleName { get; set; } = null!;
        public string ModuleDesc { get; set; } = null!;
        public string ModuleJson { get; set; } = null!;
        public string? Base64dataPicture { get; set; }

        public virtual ICollection<EventModule> EventModules { get; set; }
    }
}
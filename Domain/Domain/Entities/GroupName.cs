namespace Domain.Entities
{
    public class GroupName
    {
        public GroupName()
        {
            Groups = new HashSet<Group>();
        }

        public int IdGroupName { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Group> Groups { get; set; }
    }
}
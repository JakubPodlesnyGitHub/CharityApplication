namespace Domain.Entities
{
    public class GroupAccount
    {
        public int IdGroup { get; set; }
        public int IdAccount { get; set; }

        public virtual Group GroupNavigation { get; set; } = null!;
        public virtual Account AccountNavigation { get; set; } = null!;
    }
}
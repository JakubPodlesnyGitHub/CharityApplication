namespace Domain.Entities
{
    public partial class PrivateAccount
    {
        public int IdAccount { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }

        public virtual Account AccountNavigation { get; set; } = null!;
    }
}
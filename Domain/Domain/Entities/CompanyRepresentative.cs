namespace Domain.Entities
{
    public class CompanyRepresentative
    {
        public CompanyRepresentative()
        {
            CompanyAccountsCollection = new HashSet<CompanyAccount>();
        }

        public int IdCompanyRepresentative { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string RepresentativeMail { get; set; } = null!;
        public string? RepresentativePhone { get; set; }
        public virtual ICollection<CompanyAccount> CompanyAccountsCollection { get; set; }
    }
}
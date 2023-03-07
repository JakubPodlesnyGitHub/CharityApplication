namespace Domain.Entities
{
    public partial class CompanyAccount
    {
        public int IdAccount { get; set; }
        public string Name { get; set; } = null!;
        public string? CompanyDesc { get; set; }

        public string? Krs { get; set; }
        public string? Nip { get; set; }
        public int IdCompanyAddress { get; set; }
        public int IdCompanyRepresentative { get; set; }

        public string? BankAccount { get; set; }
        public string? CompanyWebsiteLink { get; set; }

        public virtual CompanyAddress CompanyAddressNavigation { get; set; } = null!;
        public virtual Account AccountNavigation { get; set; } = null!;
        public virtual CompanyRepresentative ComapnyRepresentativeNavigation { get; set; } = null!;
    }
}
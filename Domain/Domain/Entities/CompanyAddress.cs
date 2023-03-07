namespace Domain.Entities
{
    public class CompanyAddress
    {
        public CompanyAddress()
        {
            CompanyAccountsCollection = new HashSet<CompanyAccount>();
        }

        public int IdCompanyAddress { get; set; }
        public string Street { get; set; } = null!;
        public int BuildingNumber { get; set; }

        public int? ApartmentNumber { get; set; }
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string Country { get; set; } = null!;
        public virtual ICollection<CompanyAccount> CompanyAccountsCollection { get; set; }
    }
}
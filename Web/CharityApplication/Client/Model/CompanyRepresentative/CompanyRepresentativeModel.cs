namespace CharityApplication.Client.Model.CompanyRepresentative
{
    public class CompanyRepresentativeModel
    {
        public int? IdCompanyRepresentative { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string RepresentativeMail { get; set; } = null!;
        public string? RepresentativePhone { get; set; }
    }
}
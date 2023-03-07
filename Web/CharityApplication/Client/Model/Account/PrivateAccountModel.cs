namespace CharityApplication.Client.Model.Account
{
    public class PrivateAccountModel
    {
        public int? IdAccount { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool VerificationStatus { get; set; }
        public bool GoldSponsorBadge { get; set; }
        public string? Base64dataPicture { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
    }
}
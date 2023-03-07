namespace CharityApplication.Client.Model.Donation
{
    public class DonationModel
    {
        public string? CardNr { get; set; } = null;
        public DateTime? ExpirationDate { get; set; } = null;
        public string? CSV { get; set; } = null;
        public double Donation { get; set; }
    }
}
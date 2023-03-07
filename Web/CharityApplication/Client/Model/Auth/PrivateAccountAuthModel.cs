namespace CharityApplication.Client.Model.Auth
{
    public class PrivateAccountAuthModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RepeatedPassword { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
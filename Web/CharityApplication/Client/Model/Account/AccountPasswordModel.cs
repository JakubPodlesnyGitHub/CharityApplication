namespace CharityApplication.Client.Model.Account
{
    public class AccountPasswordModel
    {
        public string Email { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string RepeatedNewPassword { get; set; } = null!;
    }
}
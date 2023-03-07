namespace Infrastructure.Models
{
    public class EmailConfigurationModel
    {
        public string SmtpHost { get; set; }
        public string EmailSenderName { get; set; }
        public int SmtpPort { get; set; }
        public string EmailFrom { get; set; }
        public string SmtpPassword { get; set; }
        public int RetryCount { get; set; }
    }
}
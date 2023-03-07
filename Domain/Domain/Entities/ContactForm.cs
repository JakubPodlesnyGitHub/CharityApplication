namespace Domain.Entities
{
    public class ContactForm
    {
        public int IdContactForm { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Mail { get; set; } = null!;
    }
}
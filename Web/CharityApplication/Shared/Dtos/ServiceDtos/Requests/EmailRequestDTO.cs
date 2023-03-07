namespace Application.Dtos.ServiceDtos.Requests
{
    public class EmailRequestDTO
    {
        public string To { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
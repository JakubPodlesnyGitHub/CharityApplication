namespace Application.Dtos.ServiceDtos.Responses
{
    public class EmailResponseDTO
    {
        public string From { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
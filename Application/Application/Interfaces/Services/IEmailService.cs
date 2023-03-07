using Application.Dtos.ServiceDtos.Requests;

namespace Application.Interfaces.Services
{
    public interface IEmailService
    {
        public Task SendEmailAsync(EmailRequestDTO request, string? frontImage = null, string? backImage = null);
    }
}
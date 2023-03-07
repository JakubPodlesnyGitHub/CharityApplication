using Application.Dtos.ServiceDtos.Requests;
using Application.Interfaces.Services;
using Infrastructure.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailConfigurationModel> _emailConfiguration;

        public EmailService(IOptions<EmailConfigurationModel> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public async Task SendEmailAsync(EmailRequestDTO request, string? frontImage = null, string? backImage = null)
        {
            try
            {
                var newMessage = new MimeMessage();
                newMessage.Sender = new MailboxAddress(_emailConfiguration.Value.EmailSenderName, _emailConfiguration.Value.EmailFrom);
                newMessage.Subject = request.Subject;

                var multiPart = new Multipart("mixed");
                multiPart.Add(new TextPart(TextFormat.Html) { Text = request.Message });

                if (frontImage != null && backImage != null)
                {
                    var attachmentFrontDocument = new MimePart("image", "gif")
                    {
                        Content = new MimeContent(new MemoryStream(Convert.FromBase64String(frontImage.Split("base64,")[1])), ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = "Front Document",
                        IsAttachment = true
                    };
                    var attachmentBackDocument = new MimePart("image", "gif")
                    {
                        Content = new MimeContent(new MemoryStream(Convert.FromBase64String(backImage.Split("base64,")[1])), ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = "Back Document",
                        IsAttachment = true
                    };
                    multiPart.Add(attachmentBackDocument);
                    multiPart.Add(attachmentFrontDocument);
                }

                newMessage.Body = multiPart;
                newMessage.To.Add(new MailboxAddress($"{request.FirstName} {request.LastName}", request.To));

                using var smtp = new SmtpClient();

                smtp.Connect(
                    _emailConfiguration.Value.SmtpHost,
                    _emailConfiguration.Value.SmtpPort,
                    MailKit.Security.SecureSocketOptions.StartTls);

                smtp.Authenticate(
                    _emailConfiguration.Value.EmailFrom,
                    _emailConfiguration.Value.SmtpPassword);

                await smtp.SendAsync(newMessage);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
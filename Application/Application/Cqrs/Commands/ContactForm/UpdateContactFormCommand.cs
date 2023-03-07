using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.ContactForm
{
    public class UpdateContactFormCommand : IRequest<BasicContactFormDTO>
    {
        public int IdContactForm { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Mail { get; set; } = null!;

        public UpdateContactFormCommand(int idContactForm, string firstName, string lastName, string subject, string message, string mail)
        {
            IdContactForm = idContactForm;
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
            Message = message;
            Mail = mail;
        }
    }
}
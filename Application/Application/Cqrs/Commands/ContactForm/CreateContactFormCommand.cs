using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.ContactForm
{
    public class CreateContactFormCommand : IRequest<BasicContactFormDTO>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Mail { get; set; } = null!;

        public CreateContactFormCommand(string firstName, string lastName, string subject, string message, string mail)
        {
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
            Message = message;
            Mail = mail;
        }
    }
}
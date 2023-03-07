using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.ContactForm
{
    public class DeleteContactFormCommand : IRequest<BasicContactFormDTO>
    {
        public int IdContactForm { get; set; }

        public DeleteContactFormCommand(int idContactForm)
        {
            IdContactForm = idContactForm;
        }
    }
}
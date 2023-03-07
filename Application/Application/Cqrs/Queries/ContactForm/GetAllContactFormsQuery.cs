using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.ContactForm
{
    public class GetAllContactFormsQuery : IRequest<List<BasicContactFormDTO>>
    {
    }
}
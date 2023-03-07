using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.ContactForm
{
    public class GetContactFormByIdQuery : IRequest<BasicContactFormDTO>
    {
        public int Id { get; set; }

        public GetContactFormByIdQuery(int id)
        {
            Id = id;
        }
    }
}
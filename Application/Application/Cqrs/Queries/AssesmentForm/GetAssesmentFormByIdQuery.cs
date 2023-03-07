using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.AssesmentForm
{
    public class GetAssesmentFormByIdQuery : IRequest<BasicAssesmentFormDTO>
    {
        public int Id { get; set; }

        public GetAssesmentFormByIdQuery(int id)
        {
            Id = id;
        }
    }
}
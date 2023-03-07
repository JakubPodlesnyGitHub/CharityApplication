using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.AssesmentForm
{
    public class GetAssesmentFormsByEventIdQuery : IRequest<List<BasicAssesmentFormDTO>>
    {
        public int IdEvent { get; set; }

        public GetAssesmentFormsByEventIdQuery(int idEvent)
        {
            IdEvent = idEvent;
        }
    }
}
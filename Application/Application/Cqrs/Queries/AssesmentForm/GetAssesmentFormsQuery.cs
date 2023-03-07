using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.AssesmentForm
{
    public class GetAssesmentFormsQuery : IRequest<List<BasicAssesmentFormDTO>>
    {
    }
}
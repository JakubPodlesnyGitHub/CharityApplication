using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.AssesmentForm
{
    public class DeleteAssesmentFormCommand : IRequest<BasicAssesmentFormDTO>
    {
        public int IdAssesmentForm { get; set; }

        public DeleteAssesmentFormCommand(int idAssesmentForm)
        {
            IdAssesmentForm = idAssesmentForm;
        }
    }
}
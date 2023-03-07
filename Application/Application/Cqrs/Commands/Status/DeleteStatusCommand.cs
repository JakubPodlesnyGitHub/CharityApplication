using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Status
{
    public class DeleteStatusCommand : IRequest<BasicStatusDTO>
    {
        public int IdStatus { get; set; }

        public DeleteStatusCommand(int idStatus)
        {
            IdStatus = idStatus;
        }
    }
}
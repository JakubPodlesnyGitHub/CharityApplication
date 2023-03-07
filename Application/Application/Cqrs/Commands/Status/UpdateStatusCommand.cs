using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Status
{
    public class UpdateStatusCommand : IRequest<BasicStatusDTO>
    {
        public int IdStatus { get; set; }
        public string Name { get; set; } = null!;

        public UpdateStatusCommand(int idStatus, string name)
        {
            IdStatus = idStatus;
            Name = name;
        }
    }
}
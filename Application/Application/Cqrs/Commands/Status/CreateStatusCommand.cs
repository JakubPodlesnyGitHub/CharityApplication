using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Status
{
    public class CreateStatusCommand : IRequest<BasicStatusDTO>
    {
        public string Name { get; set; } = null!;

        public CreateStatusCommand(string name)
        {
            Name = name;
        }
    }
}
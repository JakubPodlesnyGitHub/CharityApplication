using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.GroupName
{
    public class CreateGroupNameCommand : IRequest<BasicGroupNameDTO>
    {
        public string Name { get; set; } = null!;

        public CreateGroupNameCommand(string name)
        {
            Name = name;
        }
    }
}
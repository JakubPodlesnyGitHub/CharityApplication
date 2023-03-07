using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.GroupName
{
    public class UpdateGroupNameCommand : IRequest<BasicGroupNameDTO>
    {
        public int IdGroupName { get; set; }
        public string Name { get; set; } = null!;

        public UpdateGroupNameCommand(int idGroupName, string name)
        {
            IdGroupName = idGroupName;
            Name = name;
        }
    }
}
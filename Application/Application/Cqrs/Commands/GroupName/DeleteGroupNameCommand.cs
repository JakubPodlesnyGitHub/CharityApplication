using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.GroupName
{
    public class DeleteGroupNameCommand : IRequest<BasicGroupNameDTO>
    {
        public int IdGroupName { get; set; }

        public DeleteGroupNameCommand(int idGroupName)
        {
            IdGroupName = idGroupName;
        }
    }
}
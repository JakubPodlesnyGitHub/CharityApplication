using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Group
{
    public class DeleteGroupCommand : IRequest<BasicGroupDTO>
    {
        public int IdGroup { get; set; }

        public DeleteGroupCommand(int idGroup)
        {
            IdGroup = idGroup;
        }
    }
}
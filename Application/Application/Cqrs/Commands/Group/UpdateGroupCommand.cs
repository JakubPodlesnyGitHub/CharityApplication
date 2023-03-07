using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Group
{
    public class UpdateGroupCommand : IRequest<BasicGroupDTO>
    {
        public int IdGroup { get; set; }
        public int IdGroupName { get; set; }
        public int NumberOfParticipants { get; set; }
        public string Description { get; set; } = null!;
        public bool GroupType { get; set; }
        public string? Base64dataPicture { get; set; }
        public int IdGroupOwner { get; set; }
    }
}
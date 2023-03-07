using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Group
{
    public class CreateGroupCommand : IRequest<BasicGroupDTO>
    {
        public int IdGroupName { get; set; }
        public int NumberOfParticipants { get; set; }
        public string Description { get; set; } = null!;
        public bool GroupType { get; set; }
        public string? Base64dataPicture { get; set; }
        public int IdGroupOwner { get; set; }

        public CreateGroupCommand(int idGroupName, int numberOfParticipants, string description, bool groupType, string? base64dataPicture, int idGroupOwner)
        {
            IdGroupName = idGroupName;
            NumberOfParticipants = numberOfParticipants;
            Description = description;
            GroupType = groupType;
            Base64dataPicture = base64dataPicture;
            IdGroupOwner = idGroupOwner;
        }
    }
}
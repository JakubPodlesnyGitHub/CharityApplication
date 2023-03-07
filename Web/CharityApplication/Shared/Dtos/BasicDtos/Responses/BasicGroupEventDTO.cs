using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Model.Error;

namespace CharityApplication.Shared.Dtos.BasicDtos.Responses
{
    public class BasicGroupEventDTO : ErrorResponseDTO
    {
        public BasicGroupDTO Group { get; set; } = null!;
        public BasicEventDTO Event { get; set; } = null!;
        public bool IfParticipantEvent { get; set; }
        public DateTime GroupEventCreation { get; set; }
    }
}
using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicEventAccountDTO : ErrorResponseDTO
    {
        public BasicAccountDTO Account { get; set; } = null!;
        public BasicEventDTO Event { get; set; } = null!;
        public bool IfParticipantEvent { get; set; }
        public int SubsidyAmount { get; set; }
        public DateTime EventCreation { get; set; }
    }
}
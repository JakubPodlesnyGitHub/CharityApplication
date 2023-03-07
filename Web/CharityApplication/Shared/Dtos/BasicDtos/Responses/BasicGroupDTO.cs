using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicGroupDTO : ErrorResponseDTO
    {
        public int IdGroup { get; set; }
        public BasicGroupNameDTO GroupName { get; set; } = null!;
        public int NumberOfParticipants { get; set; }
        public int Points { get; set; }
        public string Description { get; set; } = null!;
        public bool GroupType { get; set; }
        public string? Base64dataPicture { get; set; }
        public int IdGroupOwner { get; set; }
    }
}
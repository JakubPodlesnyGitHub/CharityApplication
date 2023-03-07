using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicBadgeDTO : ErrorResponseDTO
    {
        public int IdBadge { get; set; }
        public string Name { get; set; } = null!;
        public string BadgeGoal { get; set; } = null!;
        public string? Base64dataPicture { get; set; }
        public int Pointstreshold_User { get; set; }
        public int Pointstreshold_Group { get; set; }
    }
}
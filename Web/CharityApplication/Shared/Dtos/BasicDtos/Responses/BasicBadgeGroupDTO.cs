using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicBadgeGroupDTO : ErrorResponseDTO
    {
        public BasicBadgeDTO Badge { get; set; } = null!;
        public BasicGroupDTO Group { get; set; } = null!;
        public DateTime DateOfReceivingTheBadge { get; set; }
    }
}
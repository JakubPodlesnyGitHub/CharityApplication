using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicBadgeAccountDTO : ErrorResponseDTO
    {
        public BasicBadgeDTO Badge { get; set; } = null!;
        public BasicAccountDTO Account { get; set; } = null!;
        public DateTime DateOfReceivingTheBadge { get; set; }
    }
}
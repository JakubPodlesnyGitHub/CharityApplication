using Application.Dtos.BasicDtos.Responses;

namespace Application.Dtos.ExtendedDtos.Responses
{
    public class CompanyAccountWithBadgePointsDTO : BasicAccountDTO
    {
        public int PointsSum { get; set; }
        public int NumberOfBadges { get; set; }
    }
}
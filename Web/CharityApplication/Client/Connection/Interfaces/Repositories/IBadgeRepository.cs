using Application.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IBadgeRepository
    {
        public Task<List<BasicBadgeDTO>> GetBadges();
    }
}
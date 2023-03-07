using Application.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IBadgeAccountRepository
    {
        public Task<List<BasicBadgeAccountDTO>> GetAccountBadgesById(int accountId);
    }
}
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IBadgeAccountRepository : IBaseRepository<BadgeAccount>
    {
        public Task<List<BadgeAccount>> GetAccountBadgesByAccountId(int accountId);

        public Task<BadgeAccount> GetBadgeAccountWithRelationalEntities(int idAccount, int idBadge);

        public Task<List<BadgeAccount>> GetBadgesAccountsWithRelationalEntities();
    }
}
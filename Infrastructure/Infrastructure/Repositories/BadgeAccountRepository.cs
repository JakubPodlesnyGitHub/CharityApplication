using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BadgeAccountRepository : BaseRepository<BadgeAccount>, IBadgeAccountRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;

        public BadgeAccountRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
        }

        public async Task<List<BadgeAccount>> GetAccountBadgesByAccountId(int accountId)
        {
            var accountBadges = await _charityApplicationDbContext.BadgeAccounts.Where(ae => ae.IdAccount == accountId).ToListAsync();
            return accountBadges;
        }

        public async Task<BadgeAccount> GetBadgeAccountWithRelationalEntities(int idAccount, int idBadge)
        {
            var badgeAccount = await _charityApplicationDbContext.BadgeAccounts
                .Include(ba => ba.BadgeNavigation)
                .Include(ba => ba.AccountNavigation)
                .Where(ba => ba.IdBadge == idBadge && ba.IdAccount == idAccount).FirstOrDefaultAsync();
            return badgeAccount;
        }

        public async Task<List<BadgeAccount>> GetBadgesAccountsWithRelationalEntities()
        {
            var badgesAccounts = await _charityApplicationDbContext.BadgeAccounts
                .Include(ba => ba.BadgeNavigation)
                .Include(ba => ba.AccountNavigation).ToListAsync();
            return badgesAccounts;
        }
    }
}
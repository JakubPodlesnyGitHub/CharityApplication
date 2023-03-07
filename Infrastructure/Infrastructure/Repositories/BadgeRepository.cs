using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BadgeRepository : BaseRepository<Badge>, IBadgeRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;

        public BadgeRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
        }

        public async Task<List<Badge>> GetBadgesByAccountId(int accountId)
        {
            return await _charityApplicationDbContext.Badges.Where(b => !b.BadgeAccounts.Any(ba => ba.IdAccount == accountId && ba.IdBadge == b.IdBadge)).ToListAsync();
        }

        public async Task<List<Badge>> GetBadgesByGroupId(int groupId)
        {
            return await _charityApplicationDbContext.Badges.Where(b => !b.BadgeGroups.Any(ba => ba.IdGroup == groupId && ba.IdBadge == b.IdBadge)).ToListAsync();
        }
    }
}
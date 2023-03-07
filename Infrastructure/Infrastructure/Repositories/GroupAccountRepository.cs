using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GroupAccountRepository : BaseRepository<GroupAccount>, IGroupAccountRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;

        public GroupAccountRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
        }

        public async Task<List<GroupAccount>> GetGroupAccountsByGroupId(int groupId)
        {
            var groupAccounts = await _charityApplicationDbContext.GroupAccounts.Where(ae => ae.IdGroup == groupId).ToListAsync();
            return groupAccounts;
        }

        public async Task<List<GroupAccount>> GetPartcipantOfEventGroupMembers(int groupId, int eventId)
        {
            var groupsAccounts = await _charityApplicationDbContext.GroupAccounts
                .Where(ga => ga.IdGroup == groupId)
                .Include(ga => ga.AccountNavigation)
                .ThenInclude(a => a.AccountEventCollection.Any(ae => ae.IdEvent == eventId && ae.IfPartcipantPresent))
                .ToListAsync();
            return groupsAccounts;
        }
    }
}
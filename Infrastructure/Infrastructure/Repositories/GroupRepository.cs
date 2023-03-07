using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;

        public GroupRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
        }

        public async Task<List<GroupWithBadgePointsDTO>> GetTopgGroupsWithBadgePoints(int numberOfGroups)
        {
            var topGroups = await _charityApplicationDbContext.Groups
            .Include(g => g.GroupNameNavigation)
            .Include(g => g.BadgeGroups)
            .ThenInclude(bg => bg.BadgeNavigation)
            .Select(g => new GroupWithBadgePointsDTO
            {
                IdGroup = g.IdGroup,
                NumberOfParticipants = g.NumberOfParticipants,
                Description = g.Description,
                GroupType = g.GroupType,
                GroupName = new BasicGroupNameDTO { IdGroupName = g.GroupNameNavigation.IdGroupName, Name = g.GroupNameNavigation.Name },
                Base64dataPicture = g.Base64dataPicture,
                PointsSum = g.BadgeGroups.Sum(bg => bg.BadgeNavigation.Pointstreshold_Group),
                NumberOfBadges = g.BadgeGroups.Count()
            })
            .OrderByDescending(p => p.PointsSum)
            .Take(numberOfGroups)
            .ToListAsync();

            return topGroups;
        }

        public async Task<List<Group>> GetPublicGroups()
        {
            return await _charityApplicationDbContext.Groups
                .Where(g => !g.GroupType)
                .ToListAsync();
        }

        public async Task<List<Group>> GetPublicPrivateGroups(int accountId)
        {
            return await _charityApplicationDbContext.Groups
                .Where(g => !g.GroupType || g.IdGroupOwner == accountId)
                .ToListAsync();
        }

        public async Task<List<Group>> GetGroupsByAccountId(int accountId)
        {
            return await _charityApplicationDbContext.Groups
                .Include(g => g.GroupAccounts)
                .Where(e => e.GroupAccounts.Any(ga => ga.IdAccount == accountId))
                .ToListAsync();
        }

        public async Task<List<Group>> GetGroupsByEventId(int eventId)
        {
            return await _charityApplicationDbContext.Groups
                .Include(g => g.GroupEvents)
                .Where(g => g.GroupEvents.Any(ge => ge.IdEvent == eventId))
                .ToListAsync();
        }
    }
}
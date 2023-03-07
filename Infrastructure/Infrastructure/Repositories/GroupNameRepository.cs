using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories
{
    public class GroupNameRepository : BaseRepository<GroupName>, IGroupNameRepository
    {
        public GroupNameRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
        }
    }
}
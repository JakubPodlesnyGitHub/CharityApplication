using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        public ModuleRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
        }
    }
}
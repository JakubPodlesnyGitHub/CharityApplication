using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories
{
    public class CompanyAddressRepository : BaseRepository<CompanyAddress>, ICompanyAddressRepository
    {
        public CompanyAddressRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
        }
    }
}
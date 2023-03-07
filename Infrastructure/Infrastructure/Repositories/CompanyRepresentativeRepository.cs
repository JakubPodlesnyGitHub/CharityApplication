using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories
{
    public class CompanyRepresentativeRepository : BaseRepository<CompanyRepresentative>, ICompanyRepresentativeRepository
    {
        public CompanyRepresentativeRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
        }
    }
}
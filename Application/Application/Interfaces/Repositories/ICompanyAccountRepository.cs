using Application.Dtos.ExtendedDtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ICompanyAccountRepository : IBaseRepository<CompanyAccount>
    {
        public Task<List<CompanyAccountWithBadgePointsDTO>> GetTopCompanyAccountsWithBadgePoints(int numberOfCompanyAccounts);
    }
}
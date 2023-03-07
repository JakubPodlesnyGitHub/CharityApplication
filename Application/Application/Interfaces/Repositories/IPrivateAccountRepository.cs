using Application.Dtos.ExtendedDtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IPrivateAccountRepository : IBaseRepository<PrivateAccount>
    {
        public Task<List<PrivateAccountWithBadgePointsDTO>> GetTopPrivateAccountsWithBadgePoints(int numberOfPeople);
    }
}
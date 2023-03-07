using Application.Dtos.ExtendedDtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        public Task<Account?> GetAccountWithRelationalEntitiesAsync(int accountId);

        public Task<List<Account>> GetRelatedAccountsByEventId(int eventId);

        public Task<List<Account>> GetRelatedAccountsByGroupId(int groupId);

        public Task<Account> GetAccountByEmailAsync(string email);

        public Task<List<AccountsWithMostCreatedEventsDTO>> GetAccountsWithMostCreatedEvents(int numberOfAccounts);

        public Task<List<Account>> GetUnconfirmedAccountsByEventId(int eventId);
    }
}
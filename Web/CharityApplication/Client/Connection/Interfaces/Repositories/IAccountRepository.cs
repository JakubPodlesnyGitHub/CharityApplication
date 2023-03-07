using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using CharityApplication.Client.Model.Account;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        public Task<List<BasicAccountDTO>> GetAccounts();

        public Task<BasicAccountDTO> GetAccount(int accountId);

        public Task<List<BasicAccountDTO>> GetAccountsByGroupId(int groupId);

        public Task<List<BasicAccountDTO>> GetAccountsByEventId(int eventId);

        public Task<List<BasicAccountDTO>> GetUnconfirmedAccountsByEventId(int eventId);

        public Task<BasicAccountDTO> GetAccountByEmail(string email);

        public Task<List<AccountsWithMostCreatedEventsDTO>> GetTopAccountsWithMostCreatedEvents();

        public Task<List<PrivateAccountWithBadgePointsDTO>> GetTopPrivateAccountsWithMostPoints();

        public Task<List<CompanyAccountWithBadgePointsDTO>> GetTopCompanyAccountsWithMostPoints();

        public Task<BasicAccountDTO> UpdateAccount(AccountModel account);

        public Task<BasicAccountDTO> UpdateAccountPassword(AccountPasswordModel accountPassword);

        public Task<BasicAccountDTO> DeleteAccount(int accountId);
    }
}
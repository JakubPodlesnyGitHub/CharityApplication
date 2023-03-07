using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.Account;

namespace CharityApplication.Client.Connection.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpService _httpService;
        private readonly string URL = "api/Account";
        private readonly int NUMBER_OF_TOP_RECORDS = 10;

        public AccountRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BasicAccountDTO>> GetAccounts()
        {
            var response = await _httpService.Get<List<BasicAccountDTO>>($"{URL}/GetAccounts");
            return response.Response;
        }

        public async Task<BasicAccountDTO> GetAccount(int accountId)
        {
            var response = await _httpService.Get<BasicAccountDTO>($"{URL}/account/{accountId}");
            return response.Response;
        }

        public async Task<List<AccountsWithMostCreatedEventsDTO>> GetTopAccountsWithMostCreatedEvents()
        {
            var response = await _httpService.Get<List<AccountsWithMostCreatedEventsDTO>>($"{URL}/top-accounts/{NUMBER_OF_TOP_RECORDS}");
            return response.Response;
        }

        public async Task<List<PrivateAccountWithBadgePointsDTO>> GetTopPrivateAccountsWithMostPoints()
        {
            var response = await _httpService.Get<List<PrivateAccountWithBadgePointsDTO>>($"api/PrivateAccount/topprivateaccounts/{NUMBER_OF_TOP_RECORDS}");
            return response.Response;
        }

        public async Task<List<CompanyAccountWithBadgePointsDTO>> GetTopCompanyAccountsWithMostPoints()
        {
            var response = await _httpService.Get<List<CompanyAccountWithBadgePointsDTO>>($"api/CompanyAccount/topcompanyaccounts/{NUMBER_OF_TOP_RECORDS}");
            return response.Response;
        }

        public async Task<List<BasicAccountDTO>> GetUnconfirmedAccountsByEventId(int eventId)
        {
            var response = await _httpService.Get<List<BasicAccountDTO>>($"{URL}/unconfirmed-accounts-by-event/{eventId}");
            return response.Response;
        }

        public async Task<List<BasicAccountDTO>> GetAccountsByEventId(int eventId)
        {
            var response = await _httpService.Get<List<BasicAccountDTO>>($"{URL}/accounts-by-event/{eventId}");
            return response.Response;
        }

        public async Task<List<BasicAccountDTO>> GetAccountsByGroupId(int groupId)
        {
            var response = await _httpService.Get<List<BasicAccountDTO>>($"{URL}/accounts-by-group/{groupId}");
            return response.Response;
        }

        public async Task<BasicAccountDTO> GetAccountByEmail(string email)
        {
            var response = await _httpService.Get<BasicAccountDTO>($"{URL}/account-by-email/{email}");
            return response.Response;
        }

        public async Task<BasicAccountDTO> UpdateAccount(AccountModel account)
        {
            var response = await _httpService.Put<AccountModel, BasicAccountDTO>($"{URL}/UpdateAccount", account);
            return response.Response;
        }

        public async Task<BasicAccountDTO> UpdateAccountPassword(AccountPasswordModel accountPassword)
        {
            var response = await _httpService.Put<AccountPasswordModel, BasicAccountDTO>($"{URL}/UpdatePasswordAccount", accountPassword);
            return response.Response;
        }

        public async Task<BasicAccountDTO> DeleteAccount(int accountId)
        {
            var response = await _httpService.Delete<BasicAccountDTO>($"{URL}/account/{accountId}");
            return response.Response;
        }
    }
}
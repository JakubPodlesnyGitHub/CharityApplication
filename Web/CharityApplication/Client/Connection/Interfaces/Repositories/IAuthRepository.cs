using Application.Dtos.ServiceDtos.Responses;
using CharityApplication.Client.Model.Auth;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        public Task<AuthResponseDTO> RegisterPrivateUser(PrivateAccountAuthModel privateAccount);

        public Task<AuthResponseDTO> RegisterCompanyUser(CompanyAccountAuthModel companyAccount);

        public Task<AuthResponseDTO> LoginUser(LoginModel login);

        public Task<AuthResponseDTO> GoogleCallback();

        public Task Logout();

        public Task<string> RefreshToken();
    }
}
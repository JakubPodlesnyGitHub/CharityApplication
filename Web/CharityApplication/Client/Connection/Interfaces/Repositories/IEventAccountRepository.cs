using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Model.AccountEvent;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IEventAccountRepository
    {
        public Task<BasicEventAccountDTO> DeleteEventAccount(int eventId, int accountId);

        public Task<BasicEventAccountDTO> CreateEventAccount(EventAccountModel eventAccount);

        public Task<BasicEventAccountDTO> UpdateEventAccount(EventAccountModel eventAccount);

        public Task<BasicEventAccountDTO> UpdateEventAccountSubsidy(EventAccountModel eventAccount);
    }
}
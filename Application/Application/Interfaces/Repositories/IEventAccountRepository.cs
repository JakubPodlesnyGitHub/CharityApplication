using Application.Cqrs.Commands.EventAccount;
using Application.Dtos.BasicDtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IEventAccountRepository : IBaseRepository<AccountEvent>
    {
        public Task<List<BasicEventAccountDTO>> GetEventAccountsByEventId(int eventId);

        public Task<AccountEvent> InsertAccountEvent(CreateEventAccountCommand request);
    }
}
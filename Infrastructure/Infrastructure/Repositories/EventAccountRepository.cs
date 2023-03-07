using Application.Cqrs.Commands.EventAccount;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventAccountRepository : BaseRepository<AccountEvent>, IEventAccountRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;
        private readonly IMapper _mapper;

        public EventAccountRepository(CharityApplicationDbContext charityApplicationDbContext, IMapper mapper) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<BasicEventAccountDTO>> GetEventAccountsByEventId(int eventId)
        {
            var eventAccounts = await _charityApplicationDbContext.AccountEvents
                .Include(ae => ae.EventNavigation)
                .Include(ae => ae.AccountNavigation)
                .Where(ae => ae.IdEvent == eventId)
                .Select(ae => new BasicEventAccountDTO
                {
                    Account = _mapper.Map<BasicAccountDTO>(ae.AccountNavigation),
                    Event = _mapper.Map<BasicEventDTO>(ae.EventNavigation)
                }).ToListAsync();
            return eventAccounts;
        }

        public async Task<AccountEvent> InsertAccountEvent(CreateEventAccountCommand request)
        {
            var eventAccount = new AccountEvent { IdAccount = request.IdAccount, IdEvent = request.IdEvent, IfPartcipantPresent = (bool)request.IfPartcipantPresent };
            await _charityApplicationDbContext.AccountEvents.AddAsync(eventAccount);
            await _charityApplicationDbContext.SaveChangesAsync();
            return await _charityApplicationDbContext.AccountEvents
                .Include(ae => ae.EventNavigation)
                .Include(ae => ae.AccountNavigation)
                .AsTracking()
                .FirstOrDefaultAsync(x => x.IdEvent == eventAccount.IdEvent && x.IdAccount == eventAccount.IdAccount);
        }
    }
}
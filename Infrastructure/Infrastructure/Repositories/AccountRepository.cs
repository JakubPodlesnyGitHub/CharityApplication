using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;
        private readonly IMapper _mapper;

        public AccountRepository(CharityApplicationDbContext charityApplicationDbContext, IMapper mapper) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
            _mapper = mapper;
        }

        public async Task<Account?> GetAccountWithRelationalEntitiesAsync(int accountId)
        {
            return await _charityApplicationDbContext.Accounts
                            .Include(a => a.PrivateAccountNavigation)
                            .Include(a => a.CompanyAccountNavigation)
                            .ThenInclude(ca => ca.CompanyAddressNavigation)
                            .Include(ca => ca.CompanyAccountNavigation.ComapnyRepresentativeNavigation)
                            .Where(a => a.Id == accountId)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<Account>> GetRelatedAccountsByEventId(int eventId)
        {
            return await _charityApplicationDbContext.Accounts.Where(a => a.AccountEventCollection.Any(x => x.IdEvent == eventId)).ToListAsync();
        }

        public async Task<List<Account>> GetUnconfirmedAccountsByEventId(int eventId)
        {
            return await _charityApplicationDbContext.Accounts.Where(a => a.AccountEventCollection.Any(x => x.IdEvent == eventId && !x.IfPartcipantPresent)).ToListAsync();
        }

        public async Task<List<Account>> GetRelatedAccountsByGroupId(int groupId)
        {
            return await _charityApplicationDbContext.Accounts.Where(a => a.AccountGroupCollection.Any(x => x.IdGroup == groupId)).ToListAsync();
        }

        public async Task<Account> GetAccountByEmailAsync(string email)
        {
            return await GetBy(a => a.Email == email);
        }

        public async Task<List<AccountsWithMostCreatedEventsDTO>> GetAccountsWithMostCreatedEvents(int numberOfAccounts)
        {
            var topAccountsWithMostEvents = await _charityApplicationDbContext.Accounts
                .Include(a => a.PrivateAccountNavigation)
                .Include(a => a.CompanyAccountNavigation)
                .Include(a => a.AccountEventCollection)
                .ThenInclude(ae => ae.EventNavigation)
                .Select(a => new AccountsWithMostCreatedEventsDTO
                {
                    IdAccount = a.Id,
                    Email = a.Email,
                    VerificationStatus = a.VerificationStatus,
                    GoldSponsorBadge = a.GoldSponsorBadge,
                    Phone = a.PhoneNumber,
                    PrivateAccount = _mapper.Map<BasicPrivateAccountDTO>(a.PrivateAccountNavigation),
                    CompanyAccount = _mapper.Map<BasicCompanyAccountDTO>(a.CompanyAccountNavigation),
                    NumberOfEvents = a.AccountEventCollection.Count(),
                    CreatedEvents = _mapper.Map<List<BasicEventDTO>>(a.AccountEventCollection.Select(x => x.EventNavigation).ToList())
                })
                .OrderByDescending(a => a.NumberOfEvents)
                .Take(numberOfAccounts)
                .ToListAsync();
            Console.WriteLine(topAccountsWithMostEvents);
            return topAccountsWithMostEvents;
        }
    }
}
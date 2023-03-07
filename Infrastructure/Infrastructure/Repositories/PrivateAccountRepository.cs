using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PrivateAccountRepository : BaseRepository<PrivateAccount>, IPrivateAccountRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;
        private readonly IMapper _mapper;

        public PrivateAccountRepository(CharityApplicationDbContext charityApplicationDbContext, IMapper mapper) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<PrivateAccountWithBadgePointsDTO>> GetTopPrivateAccountsWithBadgePoints(int numberOfPeople)
        {
            var topPrivateAccounts = await _charityApplicationDbContext.PrivateAccounts
                .Include(p => p.AccountNavigation)
                .ThenInclude(a => a.AccountBadgeCollection)
                .ThenInclude(ab => ab.BadgeNavigation)
                .Select(p => new PrivateAccountWithBadgePointsDTO
                {
                    IdAccount = p.IdAccount,
                    Email = p.AccountNavigation.Email,
                    VerificationStatus = p.AccountNavigation.VerificationStatus,
                    GoldSponsorBadge = p.AccountNavigation.GoldSponsorBadge,
                    Phone = p.AccountNavigation.PhoneNumber,
                    PrivateAccount = _mapper.Map<BasicPrivateAccountDTO>(p),
                    PointsSum = p.AccountNavigation.AccountBadgeCollection.Sum(ab => ab.BadgeNavigation.Pointstreshold_User),
                    NumberOfBadges = p.AccountNavigation.AccountBadgeCollection.Count()
                })
                .OrderByDescending(p => p.PointsSum)
                .Take(numberOfPeople)
                .ToListAsync();

            return topPrivateAccounts;
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CompanyAccountRepository : BaseRepository<CompanyAccount>, ICompanyAccountRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;
        private readonly IMapper _mapper;

        public CompanyAccountRepository(CharityApplicationDbContext charityApplicationDbContext, IMapper mapper) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<CompanyAccountWithBadgePointsDTO>> GetTopCompanyAccountsWithBadgePoints(int numberOfCompanyAccounts)
        {
            var topCompanyAccounts = await _charityApplicationDbContext.CompanyAccounts
                .Include(c => c.AccountNavigation)
                .ThenInclude(a => a.AccountBadgeCollection)
                .ThenInclude(ab => ab.BadgeNavigation)
                .Select(c => new CompanyAccountWithBadgePointsDTO
                {
                    IdAccount = c.IdAccount,

                    CompanyAccount = _mapper.Map<BasicCompanyAccountDTO>(c),
                    PointsSum = c.AccountNavigation.AccountBadgeCollection.Sum(ab => ab.BadgeNavigation.Pointstreshold_User),
                    NumberOfBadges = c.AccountNavigation.AccountBadgeCollection.Count()
                })
                .OrderByDescending(p => p.PointsSum)
                .Take(numberOfCompanyAccounts)
                .ToListAsync();

            return topCompanyAccounts;
        }
    }
}
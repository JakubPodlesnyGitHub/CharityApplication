using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventModuleRepository : BaseRepository<EventModule>, IEventModuleRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;
        private readonly IMapper _mapper;

        public EventModuleRepository(CharityApplicationDbContext charityApplicationDbContext, IMapper mapper) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<BasicModuleDTO>> GetEventModulesListByEventId(int idEvent)
        {
            var modules = await _charityApplicationDbContext.Modules.Where(m => m.EventModules.Any(em => em.IdEvent == idEvent)).ToListAsync();
            return _mapper.Map<List<BasicModuleDTO>>(modules);
        }
    }
}
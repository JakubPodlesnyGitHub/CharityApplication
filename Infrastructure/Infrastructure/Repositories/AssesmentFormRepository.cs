using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssesmentFormRepository : BaseRepository<AssesmentForm>, IAssesmentFormRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;
        private readonly IMapper _mapper;

        public AssesmentFormRepository(CharityApplicationDbContext charityApplicationDbContext, IMapper mapper) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<BasicAssesmentFormDTO>> GetAssessmentFormsByEventId(int eventId)
        {
            return _mapper.Map<List<BasicAssesmentFormDTO>>(await _charityApplicationDbContext.AssesmentForms.Where(a => a.IdEvent == eventId).ToListAsync());
        }
    }
}
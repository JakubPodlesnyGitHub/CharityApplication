using Application.Dtos.BasicDtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAssesmentFormRepository : IBaseRepository<AssesmentForm>
    {
        public Task<List<BasicAssesmentFormDTO>> GetAssessmentFormsByEventId(int eventId);
    }
}
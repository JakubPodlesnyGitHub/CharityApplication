using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Model.AssessmentForm;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IAssessmentFormRepository
    {
        public Task<BasicAssesmentFormDTO> GetAssessmentForm(int assesmentFormId);

        public Task<BasicAssesmentFormDTO> CreateAssessmentForm(AssessmentFormModel assessmentForm);

        public Task<BasicAssesmentFormDTO> UpdateAssessmentForm(AssessmentFormModel assessmentForm);

        public Task<BasicAssesmentFormDTO> DeleteAssessmentForm(int assesmentFormId);

        public Task<List<BasicAssesmentFormDTO>> GetAssessmentFormsByEventId(int eventId);
    }
}
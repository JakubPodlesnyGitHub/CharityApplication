using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.AssessmentForm;

namespace CharityApplication.Client.Connection.Repositories
{
    public class AssessmentFormRepository : IAssessmentFormRepository
    {
        public readonly IHttpService _httpService;
        public readonly string URL = "api/AssesmentForm";

        public AssessmentFormRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<BasicAssesmentFormDTO> GetAssessmentForm(int assesmentFormId)
        {
            var response = await _httpService.Get<BasicAssesmentFormDTO>($"{URL}/assesmentForm/{assesmentFormId}");
            return response.Response;
        }

        public async Task<List<BasicAssesmentFormDTO>> GetAssessmentFormsByEventId(int eventId)
        {
            var response = await _httpService.Get<List<BasicAssesmentFormDTO>>($"{URL}/assesmentForms/{eventId}");
            return response.Response;
        }

        public async Task<BasicAssesmentFormDTO> CreateAssessmentForm(AssessmentFormModel assessmentForm)
        {
            var response = await _httpService.Post<AssessmentFormModel, BasicAssesmentFormDTO>($"{URL}/CreateAssesmentForm", assessmentForm);
            return response.Response;
        }

        public async Task<BasicAssesmentFormDTO> UpdateAssessmentForm(AssessmentFormModel assessmentForm)
        {
            var response = await _httpService.Put<AssessmentFormModel, BasicAssesmentFormDTO>($"{URL}/UpdateAssesmentForm", assessmentForm);
            return response.Response;
        }

        public async Task<BasicAssesmentFormDTO> DeleteAssessmentForm(int assesmentFormId)
        {
            var response = await _httpService.Delete<BasicAssesmentFormDTO>($"{URL}/assesmentForm/{assesmentFormId}");
            return response.Response;
        }
    }
}
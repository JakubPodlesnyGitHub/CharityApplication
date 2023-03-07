using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.AssessmentForm;
using CharityApplication.Client.Validators.AssessmentForm;
using CharityApplication.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Shared.Modules
{
    public partial class AssessmentFormComponent
    {
        public AssessmentFormModel Model { get; set; } = new AssessmentFormModel();
        public BasicAccountDTO LoggedUser { get; set; } = new BasicAccountDTO();
        public AssessmentFormModelValidator Validator { get; set; }
        private MudForm? Form;

        [Parameter]
        public int IdAccount { get; set; }

        [Parameter]
        public int IdEvent { get; set; }

        [Parameter]
        public int IdAssessmentForm { get; set; }

        [Parameter]
        public FormState FormState { get; set; }

        [Inject]
        public IAssessmentFormRepository AssessmentFormRepository { get; set; }

        private string FormTitle = string.Empty;
        private string ButtonTitle = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Validator = new AssessmentFormModelValidator();
            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                LoggedUser = JsonSerializer.Deserialize<BasicAccountDTO>(
                                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
            }

            if (FormState == FormState.CREATE)
            {
                FormTitle = "Assessment Form Creation";
                ButtonTitle = "Create Assessment Form";
                Model.IdOwner = LoggedUser.IdAccount;
                Model.Event = IdEvent;
            }
            else if (FormState == FormState.EDIT)
            {
                FormTitle = "Assessment Form Edition";
                ButtonTitle = "Update Assessment Form";
                var assessmentFormDTO = await AssessmentFormRepository.GetAssessmentForm(IdAssessmentForm);
                if (!string.IsNullOrEmpty(assessmentFormDTO.Detail))
                {
                    SnackBar.Add(assessmentFormDTO.Detail, Severity.Error);
                }
                else
                {
                    Model = ProvideAssessmentFormValues(assessmentFormDTO);
                }
            }
            await base.OnInitializedAsync();
        }

        private AssessmentFormModel ProvideAssessmentFormValues(BasicAssesmentFormDTO assesmentFormDTO)
        {
            return new AssessmentFormModel
            {
                IdAssesmentForm = assesmentFormDTO.IdAssesmentForm,
                Mail = assesmentFormDTO.Mail,
                EventRate = assesmentFormDTO.EventRate,
                Subject = assesmentFormDTO.Subject,
                AppRate = assesmentFormDTO.AppRate,
                Message = assesmentFormDTO.Message,
                Event = assesmentFormDTO.Event,
                IdOwner = assesmentFormDTO.IdOwner
            };
        }

        public async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                if (FormState == FormState.CREATE)
                {
                    await CreateAssessmentForm();
                }
                else
                {
                    await UpdateAssessmentForm();
                }
            }
        }

        private async Task CreateAssessmentForm()
        {
            BasicAssesmentFormDTO assesmentFormDTO = await AssessmentFormRepository.CreateAssessmentForm(Model);
            if (string.IsNullOrEmpty(assesmentFormDTO.Detail))
            {
                SnackBar.Add(assesmentFormDTO.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo($"/event/{IdEvent}");
                SnackBar.Add("Assessment form creation succeed", Severity.Success);
            }
        }

        private async Task UpdateAssessmentForm()
        {
            BasicAssesmentFormDTO assesmentFormDTO = await AssessmentFormRepository.UpdateAssessmentForm(Model);
            if (string.IsNullOrEmpty(assesmentFormDTO.Detail))
            {
                SnackBar.Add(assesmentFormDTO.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo($"/event/{IdEvent}");
                SnackBar.Add("Assessment form update succeed", Severity.Success);
            }
        }
    }
}
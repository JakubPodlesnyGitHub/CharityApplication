using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Shared.Dialog;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Shared.Modules
{
    public partial class AssessmentFormListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Parameter]
        public BasicEventDTO EventDTO { get; set; }

        [Parameter]
        public int IdLoggedAccount { get; set; }

        [Inject]
        public IAssessmentFormRepository AssessmentFormRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        public List<BasicAssesmentFormDTO> AssesmentForms { get; set; } = new List<BasicAssesmentFormDTO>();

        protected override async Task OnInitializedAsync()
        {
            AssesmentForms = await AssessmentFormRepository.GetAssessmentFormsByEventId(EventDTO.IdEvent);
            await base.OnInitializedAsync();
        }

        private async Task<BasicAssesmentFormDTO> Delete(int idAssessmentForm)
        {
            BasicAssesmentFormDTO assesmentFormDTO = null;
            var dialog = ShowDialog();
            var dialogResult = await dialog.Result;
            if (!dialogResult.Cancelled)
            {
                assesmentFormDTO = await AssessmentFormRepository.DeleteAssessmentForm(idAssessmentForm);
                if (string.IsNullOrEmpty(assesmentFormDTO.Detail))
                {
                    AssesmentForms.RemoveAll(x => x.IdAssesmentForm == assesmentFormDTO.IdAssesmentForm);
                    StateHasChanged();
                    SnackBar.Add("Assessment Form has been deleted successfully", Severity.Success);
                }
                else
                {
                    SnackBar.Add(assesmentFormDTO.Detail, Severity.Error);
                }
            }
            return assesmentFormDTO;
        }

        private IDialogReference ShowDialog()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete assessment form? This process cannot be undone.");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            return DialogService.Show<ConfirmationDialogComponent>("Delete", parameters, options);
        }
    }
}
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Pages.AssessmentForm
{
    public partial class AssessmentFormPage
    {
        [Parameter]
        public int IdAssessmentForm { get; set; }

        [Parameter]
        public int IdEvent { get; set; }
    }
}
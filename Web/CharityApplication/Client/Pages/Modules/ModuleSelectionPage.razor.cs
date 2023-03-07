using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Pages.Modules
{
    public partial class ModuleSelectionPage
    {
        [Parameter]
        public int IdEvent { get; set; }

        [Parameter]
        public bool EventEdition { get; set; } = false;
    }
}
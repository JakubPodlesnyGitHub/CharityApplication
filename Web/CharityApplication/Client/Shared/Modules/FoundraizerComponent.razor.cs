using CharityApplication.Shared.Model.JsonWrappers.Module.BasicModules;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace CharityApplication.Client.Shared.Modules
{
    public partial class FoundraizerComponent
    {
        [Parameter]
        public FoundraizerWrapper FoundraizerWrapper { get; set; }

        private string MudTextString = string.Empty;
        private IJSObjectReference JSObject;

        protected override async Task OnInitializedAsync()
        {
            JSObject = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./srcipts/script.js");
            await base.OnInitializedAsync();
        }

        public async Task CopyToClipboard()
        {
            await JSObject.InvokeVoidAsync("copyTextToClipboard", MudTextString);
            SnackBar.Add("Foundraiser data has been copied", Severity.Info);
        }
    }
}
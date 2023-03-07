using Microsoft.JSInterop;
using MudBlazor;

namespace CharityApplication.Client.Pages.Information
{
    public partial class InformationPage
    {
        private string Mail = "example12345r@gmail.com";
        private IJSObjectReference JSObject;

        protected override async Task OnInitializedAsync()
        {
            JSObject = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./srcipts/script.js");
            await base.OnInitializedAsync();
        }
        public async Task CopyToClipboard()
        {
            await JSObject.InvokeVoidAsync("copyTextToClipboard", Mail);
            SnackBar.Add("Mail has been copied", Severity.Info);
        }
    }
}
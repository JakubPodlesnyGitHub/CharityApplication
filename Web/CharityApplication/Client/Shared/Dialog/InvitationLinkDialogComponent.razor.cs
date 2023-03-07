using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using static QRCoder.PayloadGenerator;

namespace CharityApplication.Client.Shared.Dialog
{
    public partial class InvitationLinkDialogComponent
    {
        [CascadingParameter]
        public MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public string Link { get; set; } = string.Empty;

        private bool IsDisabled = true;
        private bool SwitchMarked = false;
        private IJSObjectReference JSObject;

        protected override async Task OnInitializedAsync()
        {
            JSObject = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./srcipts/script.js");
            await base.OnInitializedAsync();
        }

        public void Switch(bool value)
        {
            IsDisabled = !value;
            SwitchMarked = !SwitchMarked;
        }

        public async Task CopyToClipboard()
        {
            await JSObject.InvokeVoidAsync("copyTextToClipboard", Link);
            SnackBar.Add("Link has been copied", Severity.Info);
        }

        private void Close() => MudDialog.Close(DialogResult.Ok(true));
    }
}
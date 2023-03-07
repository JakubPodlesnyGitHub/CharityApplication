using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Shared.Dialog
{
    public partial class ConfirmationDialogComponent
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        [Parameter] public string ContentText { get; set; }

        [Parameter] public string ButtonText { get; set; }

        [Parameter] public Color Color { get; set; }

        private void Submit() => MudDialog.Close(DialogResult.Ok(true));

        private void Cancel() => MudDialog.Cancel();
    }
}
using CharityApplication.Client.Connection.Interfaces.Services;
using CharityApplication.Client.Model.AccountEvent;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Shared.Dialog
{
    public partial class QrDialogComponent
    {
        [Parameter]
        public EventAccountModel Model { get; set; }

        [CascadingParameter]
        public MudDialogInstance? MudDialog { get; set; }

        [Inject]
        public IQrCodeService QrCodeService { get; set; }

        private string URL => $"{NavigationManager.BaseUri}redirection/{Model.IdAccount}/{Model.IdEvent}/{Model.IfPartcipantPresent}";
        private ObjectFit ImageFit { get; set; } = ObjectFit.Cover;
        public string QrCodeStr { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var qrCodeDTO = await QrCodeService.CreateQrCode(URL);
            QrCodeStr = qrCodeDTO.QrCodeBase64;
            await base.OnInitializedAsync();
        }
    }
}
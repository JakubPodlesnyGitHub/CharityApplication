using Application.Dtos.ServiceDtos.Requests;
using Application.Dtos.ServiceDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Connection.Interfaces.Services;
using CharityApplication.Client.Shared.Dialog;
using CharityApplication.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Pages.Auth
{
    public partial class VerificationPage
    {
        [Parameter]
        public int IdAccount { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public IVerificationService VerificationService { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        public DocumentType DocumentType { get; set; }
        public string FrontDocument { get; set; } = string.Empty;
        public string BackDocument { get; set; } = string.Empty;

        private async Task UploadFrontImage()
        {
            var parameters = new DialogParameters();
            parameters.Add("ButtonText", "Upload");
            parameters.Add("ImageURL", FrontDocument is not null ? FrontDocument : null);
            parameters.Add("Color", Color.Success);

            var dialog = DialogService.Show<UploadImageDialogComponent>("Image Upload", parameters);
            var dialogResult = await dialog.Result;
            if (!dialogResult.Cancelled)
            {
                FrontDocument = dialogResult.Data.ToString();
            }
        }

        private async Task UploadBackImage()
        {
            var parameters = new DialogParameters();
            parameters.Add("ButtonText", "Upload");
            parameters.Add("ImageURL", BackDocument is not null ? BackDocument : null);
            parameters.Add("Color", Color.Success);

            var dialog = DialogService.Show<UploadImageDialogComponent>("Image Upload", parameters);
            var dialogResult = await dialog.Result;
            if (!dialogResult.Cancelled)
            {
                BackDocument = dialogResult.Data.ToString();
            }
        }

        private async Task Submit()
        {
            if (!string.IsNullOrEmpty(FrontDocument) && !string.IsNullOrEmpty(BackDocument))
            {
                var verifcationResponse = await VerificationService.VerifyAcccount(new VerificationRequestDTO
                {
                    IdAccount = IdAccount,
                    FrontDocumentImage = FrontDocument,
                    BackDocumentImage = BackDocument,
                    DocumentType = DocumentType,
                    AccountProfileLink = $"{NavigationManager.BaseUri}account/{IdAccount}"
                });
                if (!string.IsNullOrEmpty(verifcationResponse.Detail))
                {
                    SnackBar.Add(verifcationResponse.Detail, Severity.Error);
                }
                else
                {
                    await VerifyResponse(verifcationResponse);
                }
            }
        }

        private async Task VerifyResponse(VerifcationResponseDTO response)
        {
            if (response is not null && response.IsVerificationSuccessful)
            {
                await LocalStorageService.RemoveItemAsync("loggedUser");
                await LocalStorageService.SetItemAsStringAsync("loggedUser",
                    JsonSerializer.Serialize(await AccountRepository.GetAccount(IdAccount),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
                SnackBar.Add(response.Msg, Severity.Success);
                NavigationManager.NavigateTo($"/account/{IdAccount}");
            }
            else
            {
                SnackBar.Add(response.ErrorMsg, Severity.Error);
            }
        }
    }
}
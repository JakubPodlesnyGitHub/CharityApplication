using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.Account;
using CharityApplication.Client.Shared.Dialog;
using CharityApplication.Client.Validators.Account;
using CharityApplication.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Shared.Account
{
    public partial class AccountFormComponent
    {
        public IMask EmailMask = RegexMask.Email();
        public MudForm? Form;
        public AccountModel Model = null;
        public AccountModelValidator Validator = new AccountModelValidator();

        [Parameter]
        public int IdAccount { get; set; }

        [Parameter]
        public FormState FormState { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private string FormTitle = string.Empty;
        private string ButtonTitle = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (FormState == FormState.EDIT)
            {
                FormTitle = "Account Edition";
                ButtonTitle = "Update Account";
            }

            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                Model = JsonSerializer.Deserialize<AccountModel>(
                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }

            await base.OnInitializedAsync();
        }

        public async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                if (FormState == FormState.EDIT)
                {
                    await UpdateAccount();
                }
            }
        }

        public async Task UpdateAccount()
        {
            var accountDTO = await AccountRepository.UpdateAccount(Model);
            if (!string.IsNullOrEmpty(accountDTO.Detail))
            {
                SnackBar.Add("Account update failed", Severity.Error);
            }
            else
            {
                await LocalStorageService.RemoveItemAsync("loggedUser");
                await LocalStorageService.SetItemAsStringAsync("loggedUser",
                    JsonSerializer.Serialize(await AccountRepository.GetAccount(IdAccount),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
                NavigationManager.NavigateTo($"/account/{IdAccount}");
                SnackBar.Add("Account update succeed", Severity.Success);
            }
        }

        private async Task ImageUpload()
        {
            var parameters = new DialogParameters();
            parameters.Add("ButtonText", "Upload");
            parameters.Add("ImageURL", Model.Base64dataPicture);
            parameters.Add("Color", Color.Success);

            var dialog = DialogService.Show<UploadImageDialogComponent>("Image Upload", parameters);
            var dialogResult = await dialog.Result;
            if (!dialogResult.Cancelled)
            {
                Model.Base64dataPicture = dialogResult.Data.ToString();
            }
        }
    }
}
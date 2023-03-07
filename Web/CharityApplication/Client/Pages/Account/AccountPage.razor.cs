using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Connection.Interfaces.Services;
using CharityApplication.Client.Shared.Dialog;
using CharityApplication.Shared.Dtos.ServiceDtos.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection.Metadata;
using System.Text.Json;

namespace CharityApplication.Client.Pages.Account
{
    public partial class AccountPage
    {
        [Parameter]
        public int IdAccount { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }
        
        [Inject]
        public IAuthRepository AuthRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }
        public BasicAccountDTO Account { get; set; } = new BasicAccountDTO();

        public BasicAccountDTO LoggedUser { get; set; } = new BasicAccountDTO();

        protected override async Task OnInitializedAsync()
        {
            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                LoggedUser = JsonSerializer.Deserialize<BasicAccountDTO>(
                                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
            }
            Account = await AccountRepository.GetAccount(IdAccount);
            await base.OnInitializedAsync();
        }

        private async Task<BasicAccountDTO> DeleteAccount()
        {
            BasicAccountDTO accountDTO = null;
            var dialog = ShowDialog();
            if (dialog.Result.IsCompletedSuccessfully)
            {
                accountDTO = await AccountRepository.DeleteAccount(Account.IdAccount);
                if (string.IsNullOrEmpty(accountDTO.Detail))
                {
                    await AuthRepository.Logout();
                    SnackBar.Add("Account has been deleted successfully", Severity.Success);
                }
                else
                {
                    SnackBar.Add(accountDTO.Detail, Severity.Error);
                }
            }
            return accountDTO;
        }

        private IDialogReference ShowDialog()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete account? This process cannot be undone.");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            return DialogService.Show<ConfirmationDialogComponent>("Delete", parameters, options);
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.Account;
using CharityApplication.Client.Model.AccountEvent;
using CharityApplication.Client.Model.Donation;
using CharityApplication.Client.Validators.Donation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Shared.Dialog
{
    public partial class SubsidyPaymentDialogComponent
    {
        public DonationModel Model { get; set; } = new DonationModel();
        public DonationModelValidator Validator { get; set; } = null!;
        private MudForm? Form;

        [Parameter]
        public int IdLoggedUser { get; set; }

        [Parameter]
        public int IdEvent { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public IEventAccountRepository EventAccountRepository { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [CascadingParameter]
        public MudDialogInstance? MudDialog { get; set; }

        private readonly double MIN_DOTATION_FOR_BADGE = 50;

        protected override async Task OnInitializedAsync()
        {
            Validator = new DonationModelValidator();
            await base.OnInitializedAsync();
        }

        private async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                BasicEventAccountDTO? eventAccount = new BasicEventAccountDTO();
                if (Model.Donation >= MIN_DOTATION_FOR_BADGE)
                {
                    eventAccount = await EventAccountRepository.UpdateEventAccountSubsidy(new EventAccountModel { IdAccount = IdLoggedUser, IdEvent = IdEvent });
                    if (eventAccount.SubsidyAmount == 5 && !string.IsNullOrEmpty(eventAccount.Detail))
                    {
                        var accountDTO = await UpdateAccount();
                        if (string.IsNullOrEmpty(accountDTO.Detail))
                        {
                            SnackBar.Add("You supported 5 events. You get a gold badge", MudBlazor.Severity.Success);
                            StateHasChanged();
                        }
                    }
                    await DialogService.ShowMessageBox("Confirmation", "Thank you for your support");
                    MudDialog.Close();
                }
            }
        }

        public async Task<BasicAccountDTO> UpdateAccount()
        {
            BasicAccountDTO accountDTO = await AccountRepository.UpdateAccount(new AccountModel { GoldSponsorBadge = true });
            if (string.IsNullOrEmpty(accountDTO.Detail))
            {
                SnackBar.Add(accountDTO.Detail, MudBlazor.Severity.Error);
            }
            else
            {
                await LocalStorageService.RemoveItemAsync("loggedUser");
                await LocalStorageService.SetItemAsStringAsync("loggedUser",
                    JsonSerializer.Serialize(await AccountRepository.GetAccount(IdLoggedUser),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
            }
            return accountDTO;
        }

        private void Cancel() => MudDialog.Cancel();
    }
}
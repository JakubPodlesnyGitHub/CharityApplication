using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.AccountEvent;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Pages.PageRedirection
{
    public partial class QrRedirectionComponent
    {
        [Parameter]
        public int IdAccount { get; set; }

        [Parameter]
        public int IdEvent { get; set; }

        [Parameter]
        public bool IfParticipate { get; set; }

        [Inject]
        public IEventAccountRepository EventAccountRepository { get; set; }
        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        public BasicAccountDTO LoggedUser { get; set; }
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
            var eventAccountDTO = await EventAccountRepository.UpdateEventAccount(new EventAccountModel
            {
                IdAccount = IdAccount,
                IdEvent = IdEvent,
                IfPartcipantPresent = IfParticipate,
            });
            
            var flag = false;
            if (string.IsNullOrEmpty(eventAccountDTO.Detail))
            {
                if (LoggedUser.Points != eventAccountDTO.Account.Points)
                {
                    await LocalStorageService.RemoveItemAsync("loggedUser");
                    await LocalStorageService.SetItemAsStringAsync("loggedUser",
                        JsonSerializer.Serialize(await AccountRepository.GetAccount(LoggedUser.IdAccount),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
                }
                bool? result = await DialogService.ShowMessageBox(
                    "Presence has been confirmed",
                    $"Account nuber {eventAccountDTO.Account.AccountCredentials} confirmed its presence at the event {eventAccountDTO.Event.EventName}.\nTo your account has been added 30 points.",
                    yesText: "Proceed"
                    );
                flag = result != null;
                StateHasChanged();
            }
            else
            {
                SnackBar.Add(eventAccountDTO.Detail, Severity.Error);
            }
            if (flag)
            {
                NavigationManager.NavigateTo($"/event/{IdEvent}");
            }
            await base.OnInitializedAsync();
        }
    }
}
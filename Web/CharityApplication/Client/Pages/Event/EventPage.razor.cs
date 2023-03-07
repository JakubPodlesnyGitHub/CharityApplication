using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Connection.Interfaces.Services;
using CharityApplication.Client.Model.AccountEvent;
using CharityApplication.Client.Shared.Dialog;
using CharityApplication.Shared.Dtos.ServiceDtos.Requests;
using CharityApplication.Shared.Model.JsonWrappers.Event;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Pages.Event
{
    public partial class EventPage
    {
        [Parameter]
        public int IdEvent { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public IEventRepository EventRepository { get; set; }

        [Inject]
        public IEventAccountRepository EventAccountRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        public BasicEventDTO Event { get; set; } = null!;
        public BasicAccountDTO LoggedUser { get; set; } = new BasicAccountDTO();
        public EventWrapper EventWrapper { get; set; } = new EventWrapper();
        public List<BasicAccountDTO> EventAccounts { get; set; } = new List<BasicAccountDTO>();

        protected override async Task OnInitializedAsync()
        {
            Event = await EventRepository.GetEvent(IdEvent);
            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                LoggedUser = JsonSerializer.Deserialize<BasicAccountDTO>(
                                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
                var eventAccountsDTO = await AccountRepository.GetAccountsByEventId(IdEvent);
                EventAccounts = eventAccountsDTO.FindAll(x => x.IdAccount == LoggedUser.IdAccount);
            }

            if (Event.JsonEvent is not null)
            {
                EventWrapper = JsonSerializer.Deserialize<EventWrapper>(Event.JsonEvent);
            }
            await base.OnInitializedAsync();
        }

        private void OpenDialogInvitationLink()
        {
            var parameters = new DialogParameters
            {
                ["Link"] = $"{NavigationManager.BaseUri}event/{IdEvent}"
            };
            DialogService.Show<InvitationLinkDialogComponent>("Event Invitation", parameters);
        }

        private IDialogReference ShowDialog()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete event? This process cannot be undone.");
            parameters.Add("ButtonText", "Event");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            return DialogService.Show<ConfirmationDialogComponent>("Delete", parameters, options);
        }

        private void ShowQrCode()
        {
            var parameters = new DialogParameters();
            parameters.Add("Model", new EventAccountModel
            {
                IdAccount = LoggedUser.IdAccount,
                IdEvent = Event.IdEvent,
                IfPartcipantPresent = true
            });

            var options = new DialogOptions() { CloseButton = true, };

            DialogService.Show<QrDialogComponent>("Qr Code", parameters, options);
            SnackBar.Add("This process may take a while...", Severity.Info);
        }

        private void ShowDonationBox()
        {
            var parameters = new DialogParameters();
            parameters.Add("IdLoggedUser", LoggedUser.IdAccount);
            parameters.Add("IdEvent", IdEvent);

            var options = new DialogOptions() { CloseButton = true };

            DialogService.Show<SubsidyPaymentDialogComponent>("Donation", parameters, options);
        }

        private async Task<BasicEventDTO> DeleteEvent()
        {
            BasicEventDTO eventDTO = null;
            var dialog = ShowDialog();
            if (dialog.Result.IsCompletedSuccessfully && Event.IdEventOwner == LoggedUser.IdAccount)
            {
                eventDTO = await EventRepository.DeleteEvent(IdEvent);
                if (!string.IsNullOrEmpty(eventDTO.Detail))
                {
                    SnackBar.Add(eventDTO.Detail, Severity.Error);
                }
                else
                {
                    NavigationManager.NavigateTo("/groups");
                    SnackBar.Add("Event has been deleted successfully", Severity.Success);
                }
            }
            else
            {
                SnackBar.Add("You cannot delete the event because you are not the owner", Severity.Warning);
            }
            return eventDTO;
        }


        private async Task<BasicEventAccountDTO> JoinToEvent()
        {
            BasicEventAccountDTO eventAccountDTO = await EventAccountRepository.CreateEventAccount(new EventAccountModel
            {
                IdAccount = LoggedUser.IdAccount,
                IdEvent = IdEvent
            });
            if (string.IsNullOrEmpty(eventAccountDTO.Detail))
            {
                if (LoggedUser.Points != eventAccountDTO.Account.Points)
                {
                    await LocalStorageService.RemoveItemAsync("loggedUser");
                    await LocalStorageService.SetItemAsStringAsync("loggedUser",
                        JsonSerializer.Serialize(await AccountRepository.GetAccount(LoggedUser.IdAccount),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
                }
                EventAccounts.Add(eventAccountDTO.Account);
                StateHasChanged();
                SnackBar.Add("From now on you are the participant of the event", Severity.Success);
            }
            else
            {
                SnackBar.Add(eventAccountDTO.Detail, Severity.Error);
            }
            return eventAccountDTO;
        }

        private async Task<BasicEventAccountDTO> LeaveTheEvent()
        {
            BasicEventAccountDTO eventAccountDTO = await EventAccountRepository.DeleteEventAccount(Event.IdEvent, LoggedUser.IdAccount);
            if (string.IsNullOrEmpty(eventAccountDTO.Detail))
            {
                if (LoggedUser.Points != eventAccountDTO.Account.Points)
                {
                    await LocalStorageService.RemoveItemAsync("loggedUser");
                    await LocalStorageService.SetItemAsStringAsync("loggedUser",
                        JsonSerializer.Serialize(await AccountRepository.GetAccount(LoggedUser.IdAccount),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
                }
                EventAccounts.RemoveAll(x => x.IdAccount == eventAccountDTO.Account.IdAccount);
                StateHasChanged();
                SnackBar.Add("You are no longer partcipant of the event", Severity.Success);
            }
            else
            {
                SnackBar.Add(eventAccountDTO.Detail, Severity.Error);
            }
            return eventAccountDTO;
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.AccountEvent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Pages.Event
{
    public partial class EventsTablePage
    {
        public string SearchValue { get; set; } = "";
        public bool DenseItems { get; set; } = false;
        private bool LoadingBar = true;
        public List<BasicEventDTO> Events { get; set; } = new List<BasicEventDTO>();
        public List<BasicEventDTO> LoggedUserEvents { get; set; } = new List<BasicEventDTO>();
        public BasicEventDTO SelectedElement { get; set; } = null;
        public BasicAccountDTO LoggedUser = new BasicAccountDTO();

        [Inject]
        public IEventRepository EventRepository { get; set; }

        [Inject]
        public IEventAccountRepository EventAccountRepository { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            HttpInterceptorService.RegisterEvent();

            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                LoggedUser = JsonSerializer.Deserialize<BasicAccountDTO>(
                                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
                
                LoggedUserEvents = await EventRepository.GetEventsByAccountId(LoggedUser.IdAccount);
            }
            Events = await EventRepository.GetEventsList();
            LoadingBar = false;
            await base.OnInitializedAsync();
        }

        private async Task<BasicEventAccountDTO> JoinToEvent(BasicEventDTO eventDTO)
        {
            BasicEventAccountDTO eventAccountDTO = await EventAccountRepository.CreateEventAccount(new EventAccountModel
            {
                IdAccount = LoggedUser.IdAccount,
                IdEvent = eventDTO.IdEvent
            });
            if (string.IsNullOrEmpty(eventAccountDTO.Detail))
            {
                LoggedUserEvents.Add(eventDTO);
                StateHasChanged();
                if (LoggedUser.Points != eventAccountDTO.Account.Points)
                {
                    await LocalStorageService.RemoveItemAsync("loggedUser");
                    await LocalStorageService.SetItemAsStringAsync("loggedUser",
                        JsonSerializer.Serialize(await AccountRepository.GetAccount(LoggedUser.IdAccount),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
                }
                SnackBar.Add("From now on you are the participant of the event", Severity.Success);
            }
            else
            {
                SnackBar.Add(eventAccountDTO.Detail, Severity.Error);
            }
            return eventAccountDTO;
        }

        private async Task<BasicEventAccountDTO> LeaveTheEvent(BasicEventDTO eventDTO)
        {
            BasicEventAccountDTO eventAccountDTO = await EventAccountRepository.DeleteEventAccount(eventDTO.IdEvent, LoggedUser.IdAccount);
            if (string.IsNullOrEmpty(eventAccountDTO.Detail))
            {
                LoggedUserEvents.RemoveAll(x => x.IdEvent == eventDTO.IdEvent);
                StateHasChanged();
                if (LoggedUser.Points != eventAccountDTO.Account.Points)
                {
                    await LocalStorageService.RemoveItemAsync("loggedUser");
                    await LocalStorageService.SetItemAsStringAsync("loggedUser",
                        JsonSerializer.Serialize(await AccountRepository.GetAccount(LoggedUser.IdAccount),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
                }
                SnackBar.Add("You are no longer partcipant of the event", Severity.Success);
            }
            else
            {
                SnackBar.Add(eventAccountDTO.Detail, Severity.Error);
            }
            return eventAccountDTO;
        }

        private bool FilterFuncShort(BasicEventDTO e) => FilterFunc(e, SearchValue);

        private async Task NavigateToSpecificEvent(TableRowClickEventArgs<BasicEventDTO> tableRowClickEventArgs)
        {
            var user = (await AuthenticationStateTask).User;
            if (user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo($"/event/{tableRowClickEventArgs.Item.IdEvent}");
            }
        }

        private bool FilterFunc(BasicEventDTO e, string SearchValue)
        {
            if (e.EventName.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (e.EventGoal.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (e.EventDesc.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (e.Status.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(SearchValue))
            {
                return true;
            }
            if ($"{e.EventMemeberLimit}".Contains(SearchValue))
            {
                return true;
            }
            return false;
        }
    }
}
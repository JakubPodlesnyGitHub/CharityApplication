using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.AccountEvent;
using CharityApplication.Client.Model.EventModel;
using CharityApplication.Client.Shared.Dialog;
using CharityApplication.Client.Validators.Event;
using CharityApplication.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Shared.Event
{
    public partial class EventFormComponent
    {
        public EventModel Model { get; set; } = new EventModel();
        public BasicAccountDTO LoggedUser { get; set; } = new BasicAccountDTO();
        public EventModelValidator Validator { get; set; } = null!;
        public List<BasicStatusDTO> Statuses { get; set; } = new List<BasicStatusDTO>();
        public MudForm? Form;

        [Parameter]
        public int IdEvent { get; set; }

        [Parameter]
        public FormState FormState { get; set; }

        [Inject]
        public IStatusRepository StatusRepository { get; set; }

        [Inject]
        public IEventRepository EventRepository { get; set; }

        [Inject]
        public IEventAccountRepository EventAccountRepository { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private string FormTitle = string.Empty;
        private string ButtonTitle = string.Empty;
        private string SelectLabelStatus = "Available Event Statues";

        protected override async Task OnInitializedAsync()
        {
            Validator = new EventModelValidator();
            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                LoggedUser = JsonSerializer.Deserialize<BasicAccountDTO>(
                                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
            }

            if (FormState == FormState.CREATE)
            {
                FormTitle = "Event Creation";
                ButtonTitle = "Create Event";
                Model.IdEventOwner = LoggedUser.IdAccount;
            }
            else
            {
                FormTitle = "Event Edition";
                ButtonTitle = "Update Event";
                var eventDTO = await EventRepository.GetEvent(IdEvent);
                if (!string.IsNullOrEmpty(eventDTO.Detail))
                {
                    SnackBar.Add(eventDTO.Detail, Severity.Error);
                }
                else
                {
                    Model = ProvideEventModelValues(eventDTO);
                }
            }

            Statuses = await StatusRepository.GetEventStatuses();
            await base.OnInitializedAsync();
        }

        private void HandleSelectValue(DateTime? date)
        {
            Model.EventEndDate = date;
            if (DateTime.Compare(DateTime.Now, Model.EventStartDate.Value) < 0)
            {
                Model.Status = Statuses.FirstOrDefault(x => x.Name.Equals("Planned"));
            }
            if (DateTime.Compare(DateTime.Now, Model.EventStartDate.Value) >= 0 && DateTime.Compare(DateTime.Now, date.Value) <= 0)
            {
                Model.Status = Statuses.FirstOrDefault(x => x.Name.Equals("In Progress"));
            }
            if (DateTime.Compare(DateTime.Now, date.Value) > 0)
            {
                Model.Status = Statuses.FirstOrDefault(x => x.Name.Equals("Ended"));
            }
        }

        private EventModel ProvideEventModelValues(BasicEventDTO eventDTO)
        {
            return new EventModel
            {
                IdEvent = eventDTO.IdEvent,
                EventName = eventDTO.EventName,
                EventGoal = eventDTO.EventGoal,
                EventStartDate = eventDTO.EventStartDate,
                EventEndDate = eventDTO.EventEndDate,
                EventMemeberLimit = eventDTO.EventMemeberLimit,
                OverSale = eventDTO.OverSale,
                EventDesc = eventDTO.EventDesc,
                JsonEvent = eventDTO.JsonEvent,
                Base64dataPicture = eventDTO.Base64dataPicture,
                Status = eventDTO.Status
            };
        }

        public async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                if (FormState == FormState.CREATE)
                {
                    await CreateEvent();
                }
                else
                {
                    await UpdateEvent();
                }
            }
        }

        private async Task CreateEvent()
        {
            BasicEventDTO eventDTO = await EventRepository.CreateEvent(Model);
            BasicEventAccountDTO eventAccountDTO = await EventAccountRepository.CreateEventAccount(new EventAccountModel
            {
                IdAccount = eventDTO.IdEventOwner,
                IdEvent = eventDTO.IdEvent,
                IfPartcipantPresent = true
            });

            if (!string.IsNullOrEmpty(eventDTO.Detail) && !string.IsNullOrEmpty(eventAccountDTO.Detail))
            {
                if (LoggedUser.Points != eventAccountDTO.Account.Points)
                {
                    await LocalStorageService.RemoveItemAsync("loggedUser");
                    await LocalStorageService.SetItemAsStringAsync("loggedUser",
                        JsonSerializer.Serialize(await AccountRepository.GetAccount(LoggedUser.IdAccount),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
                }
                StateHasChanged();
                SnackBar.Add(eventAccountDTO.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo($"/modules-selection/{eventDTO.IdEvent}");
                SnackBar.Add("Event creation succeed", Severity.Success);
            }
        }

        private async Task UpdateEvent()
        {
            BasicEventDTO eventDTO = await EventRepository.UpdateEvent(Model);
            if (!string.IsNullOrEmpty(eventDTO.Detail))
            {
                SnackBar.Add(eventDTO.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo($"/modules-selection/{eventDTO.IdEvent}/true");
                SnackBar.Add("Event update succeed", Severity.Success);
            }
        }

        private async Task ImageUpload()
        {
            var parameters = new DialogParameters();
            parameters.Add("ButtonText", "Upload");
            parameters.Add("ImageURL", Model.Base64dataPicture is not null ? Model.Base64dataPicture : null);
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
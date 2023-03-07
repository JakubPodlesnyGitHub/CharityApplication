using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.EventAnnouncement;
using CharityApplication.Client.Validators.EventAnnouncement;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using CharityApplication.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Shared.EventAnnouncement
{
    public partial class EventAnnouncementFormComponent
    {
        public EventAnnouncementModel Model { get; set; } = new EventAnnouncementModel();
        public BasicAccountDTO LoggedUser { get; set; } = new BasicAccountDTO();
        public EventAnnouncementModelValidator Validator { get; set; } = null!;
        public MudForm? Form;

        [Parameter]
        public int IdEventAnnouncement { get; set; }

        [Parameter]
        public int IdEvent { get; set; }

        [Parameter]
        public FormState FormState { get; set; }

        [Inject]
        public IEventAnnouncementRepository EventAnnouncementRepository { get; set; }

        private string FormTitle = string.Empty;
        private string ButtonTitle = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Validator = new EventAnnouncementModelValidator();
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
                FormTitle = "Event Announcement Creation";
                ButtonTitle = "Create Event Announcement";
                Model.IdOwner = LoggedUser.IdAccount;
                Model.IdEvent = IdEvent;
            }
            else
            {
                FormTitle = "Event Announcement Edition";
                ButtonTitle = "Update Event Announcement";
                var eventAnnouncementDTO = await EventAnnouncementRepository.GetEventAnnouncement(IdEventAnnouncement);
                if (!string.IsNullOrEmpty(eventAnnouncementDTO.Detail))
                {
                    SnackBar.Add(eventAnnouncementDTO.Detail, Severity.Error);
                }
                else
                {
                    Model = ProvideEventAnnouncementValues(eventAnnouncementDTO);
                }
            }
            await base.OnInitializedAsync();
        }

        private EventAnnouncementModel ProvideEventAnnouncementValues(BasicEventAnnouncementDTO eventAnnouncementDTO)
        {
            return new EventAnnouncementModel
            {
                IdEventAnnouncement = eventAnnouncementDTO.IdEventAnnouncement,
                Subject = eventAnnouncementDTO.Subject,
                Message = eventAnnouncementDTO.Message,
                IdEvent = eventAnnouncementDTO.IdEvent,
                IdOwner = eventAnnouncementDTO.IdOwner,
            };
        }

        public async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                if (FormState == FormState.CREATE)
                {
                    await CreateEventAnnouncement();
                }
                else
                {
                    await UpdateEventAnnouncement();
                }
            }
        }

        private async Task CreateEventAnnouncement()
        {
            var eventAnnouncementDTO = await EventAnnouncementRepository.CreateEventAnnouncement(Model);
            if (!string.IsNullOrEmpty(eventAnnouncementDTO.Detail))
            {
                SnackBar.Add(eventAnnouncementDTO.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo($"/event/{IdEvent}");
                SnackBar.Add("Event announcement creation succeed", Severity.Success);
            }
        }

        private async Task UpdateEventAnnouncement()
        {
            var eventAnnouncementDTO = await EventAnnouncementRepository.UpdateEventAnnouncement(Model);
            if (!string.IsNullOrEmpty(eventAnnouncementDTO.Detail))
            {
                SnackBar.Add(eventAnnouncementDTO.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo($"/event/{IdEvent}");
                SnackBar.Add("Event announcement update succeed", Severity.Success);
            }
        }
    }
}
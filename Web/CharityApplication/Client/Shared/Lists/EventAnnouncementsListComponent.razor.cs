using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Shared.Dialog;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Shared.Lists
{
    public partial class EventAnnouncementsListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Parameter]
        public int EventId { get; set; }

        [Inject]
        public IEventAnnouncementRepository EventAnnouncementRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        public List<BasicEventAnnouncementDTO> EventAnnouncements { get; set; } = new List<BasicEventAnnouncementDTO>();

        protected override async Task OnInitializedAsync()
        {
            EventAnnouncements = await EventAnnouncementRepository.GetEventAnnouncements(EventId);
            if (EventAnnouncements is not null)
            {
                EventAnnouncements = EventAnnouncements.OrderByDescending(x => x.CreationDate).ToList();
            }
            await base.OnInitializedAsync();
        }

        private async Task<BasicEventAnnouncementDTO> Delete(int idEventAnnouncement)
        {
            BasicEventAnnouncementDTO eventAnnouncementDTO = null;
            var dialog = ShowDialog();
            var dialogResult = await dialog.Result;
            if (!dialogResult.Cancelled)
            {
                eventAnnouncementDTO = await EventAnnouncementRepository.DeleteEventAnnouncement(idEventAnnouncement);
                if (string.IsNullOrEmpty(eventAnnouncementDTO.Detail))
                {
                    EventAnnouncements.RemoveAll(x => x.IdEventAnnouncement == eventAnnouncementDTO.IdEventAnnouncement);
                    StateHasChanged();
                    SnackBar.Add("Event announcement has been deleted successfully", Severity.Success);
                }
                else
                {
                    SnackBar.Add("Event announcement deletion failed", Severity.Error);
                }
            }
            return eventAnnouncementDTO;
        }

        private IDialogReference ShowDialog()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete event announcement? This process cannot be undone.");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            return DialogService.Show<ConfirmationDialogComponent>("Delete", parameters, options);
        }
    }
}
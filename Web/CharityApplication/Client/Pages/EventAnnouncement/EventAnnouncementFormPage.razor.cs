using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Pages.EventAnnouncement
{
    public partial class EventAnnouncementFormPage
    {
        [Parameter]
        public int IdEventAnnouncement { get; set; }

        [Parameter]
        public int IdEvent { get; set; }
    }
}
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Pages.GroupAnnouncement
{
    public partial class GroupAnnouncementFormPage
    {
        [Parameter]
        public int IdGroupAnnouncement { get; set; }

        [Parameter]
        public int IdGroup { get; set; }
    }
}
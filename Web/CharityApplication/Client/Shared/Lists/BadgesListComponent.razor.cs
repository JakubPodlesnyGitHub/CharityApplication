using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.Lists
{
    public partial class BadgesListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Inject]
        public IBadgeRepository BadgeRepository { get; set; }

        public List<BasicBadgeDTO> Badges { get; set; } = new List<BasicBadgeDTO>();

        protected override async Task OnInitializedAsync()
        {
            Badges = await BadgeRepository.GetBadges();
            await base.OnInitializedAsync();
        }
    }
}
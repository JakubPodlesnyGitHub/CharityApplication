using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.Lists
{
    public partial class BadgesByGroupListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Parameter]
        public BasicGroupDTO Group { get; set; }

        [Inject]
        public IBadgeGroupRepository BadgeGroupRepository { get; set; }

        public List<BasicBadgeGroupDTO> GroupBadges { get; set; } = new List<BasicBadgeGroupDTO>();

        protected override async Task OnInitializedAsync()
        {
            GroupBadges = await BadgeGroupRepository.GetGroupBadges(Group.IdGroup);
            await base.OnInitializedAsync();
        }
    }
}
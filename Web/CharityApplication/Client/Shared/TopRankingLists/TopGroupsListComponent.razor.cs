using Application.Dtos.ExtendedDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.TopRankingLists
{
    public partial class TopGroupsListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Inject]
        public IGroupRepository GroupRepository { get; set; }

        public List<GroupWithBadgePointsDTO> TopGroupsWithBadgePoints { get; set; } = new List<GroupWithBadgePointsDTO>();

        protected override async Task OnInitializedAsync()
        {
            TopGroupsWithBadgePoints = await GroupRepository.GetTopGroupsWithMostPoints();
            await base.OnInitializedAsync();
        }
    }
}
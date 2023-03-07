using Application.Dtos.ExtendedDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.TopRankingLists
{
    public partial class TopPrivateAccountsListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        public List<PrivateAccountWithBadgePointsDTO> TopPrivateAccountsWithBadgePoints { get; set; } = new List<PrivateAccountWithBadgePointsDTO>();

        protected override async Task OnInitializedAsync()
        {
            TopPrivateAccountsWithBadgePoints = await AccountRepository.GetTopPrivateAccountsWithMostPoints();
            base.OnInitialized();
        }
    }
}
using Application.Dtos.ExtendedDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.TopRankingLists
{
    public partial class TopCompanyAccountsListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        public List<CompanyAccountWithBadgePointsDTO> TopCompanyAccountsWithBadgePoints { get; set; } = new List<CompanyAccountWithBadgePointsDTO>();

        protected override async Task OnInitializedAsync()
        {
            TopCompanyAccountsWithBadgePoints = await AccountRepository.GetTopCompanyAccountsWithMostPoints();
            await base.OnInitializedAsync();
        }
    }
}
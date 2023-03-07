using Application.Dtos.ExtendedDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.TopRankingLists
{
    public partial class TopAccountsWithMostEventListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        public List<AccountsWithMostCreatedEventsDTO> TopAccountsWithMostCreatedEvents { get; set; } = new List<AccountsWithMostCreatedEventsDTO>();

        protected override async Task OnInitializedAsync()
        {
            TopAccountsWithMostCreatedEvents = await AccountRepository.GetTopAccountsWithMostCreatedEvents();
            await base.OnInitializedAsync();
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.Lists
{
    public partial class BadgesByAccountListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Parameter]
        public BasicAccountDTO LoggedUser { get; set; }

        [Inject]
        public IBadgeAccountRepository BadgeAccountRepository { get; set; }

        public List<BasicBadgeAccountDTO> AccountBadges { get; set; } = new List<BasicBadgeAccountDTO>();

        protected override async Task OnInitializedAsync()
        {
            AccountBadges = await BadgeAccountRepository.GetAccountBadgesById(LoggedUser.IdAccount);
            await base.OnInitializedAsync();
        }
    }
}
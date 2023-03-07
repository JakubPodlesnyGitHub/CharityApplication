using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.Lists
{
    public partial class AccountsByEventIdListComponent
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
        public IAccountRepository AccountRepository { get; set; }

        public List<BasicAccountDTO> Accounts { get; set; } = new List<BasicAccountDTO>();

        protected override async Task OnInitializedAsync()
        {
            Accounts = await AccountRepository.GetAccountsByEventId(EventId);
            await base.OnInitializedAsync();
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.Lists
{
    public partial class AccountsByGroupIdListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Parameter]
        public int GroupId { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        public List<BasicAccountDTO> Accounts { get; set; } = new List<BasicAccountDTO>();

        protected override async Task OnInitializedAsync()
        {
            Accounts = await AccountRepository.GetAccountsByGroupId(GroupId);
            await base.OnInitializedAsync();
        }
    }
}
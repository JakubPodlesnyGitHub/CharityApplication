using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.Lists
{
    public partial class GroupsByAccountIdListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Parameter]
        public int AccountId { get; set; }

        [Inject]
        public IGroupRepository GroupRepository { get; set; }

        public List<BasicGroupDTO> Groups { get; set; } = new List<BasicGroupDTO>();

        protected override async Task OnInitializedAsync()
        {
            Groups = await GroupRepository.GetGroupsByAccountId(AccountId);
            await base.OnInitializedAsync();
        }
    }
}
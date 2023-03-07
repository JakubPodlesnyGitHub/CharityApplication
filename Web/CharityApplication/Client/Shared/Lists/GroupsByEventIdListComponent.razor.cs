using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.Lists
{
    public partial class GroupsByEventIdListComponent
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
        public IGroupRepository GroupRepository { get; set; }

        public List<BasicGroupDTO> Groups { get; set; } = new List<BasicGroupDTO>();

        protected override async Task OnInitializedAsync()
        {
            Groups = await GroupRepository.GetGroupsByEventId(EventId);
            await base.OnInitializedAsync();
        }
    }
}
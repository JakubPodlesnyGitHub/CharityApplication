using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace CharityApplication.Client.Shared.Lists
{
    public partial class EventsByGroupIdListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public bool IfPast { get; set; } = false;

        [Parameter]
        public bool ShowEventGoal { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Parameter]
        public int GroupId { get; set; }

        [Inject]
        public IEventRepository EventRepository { get; set; }

        public List<BasicEventDTO> Events { get; set; } = new List<BasicEventDTO>();

        protected override async Task OnInitializedAsync()
        {
            var eventsDTO = await EventRepository.GetEventsByGroupId(GroupId);
            if (IfPast)
            {
                Events = eventsDTO.FindAll(x => DateTime.Compare(DateTime.Now, x.EventEndDate.Value) > 0);
            }
            else
            {
                Events = eventsDTO.FindAll(x => DateTime.Compare(DateTime.Now, x.EventStartDate.Value) >= 0 && DateTime.Compare(DateTime.Now, x.EventEndDate.Value) <= 0);
            }
            await base.OnInitializedAsync();
        }
    }
}
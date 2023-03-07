using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.EventModel;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Shared.CommonComponents
{
    public partial class EventTimelineComponent
    {
        [Parameter]
        public BasicEventDTO Event { get; set; }

        [Inject]
        public IEventRepository EventRepository { get; set; }

        [Inject]
        public IStatusRepository StatusRepository { get; set; }

        public List<BasicStatusDTO> StatusList { get; set; } = new List<BasicStatusDTO>();
        public bool planned, inprogress, ended;
        private DateTime CurrentDate = DateTime.Now;

        protected override async Task OnInitializedAsync()
        {
            StatusList = await StatusRepository.GetEventStatuses();
            planned = await IsCurrentDateBeforeStart();
            inprogress = await IsCurrentDateBetween();
            ended = await IsCurrentDateAfterEnd();
            await base.OnInitializedAsync();
        }

        private async Task<bool> IsCurrentDateBeforeStart()
        {
            bool result = DateTime.Compare(CurrentDate, Event.EventStartDate.Value) < 0;
            if (result && !Event.Status.Name.Equals("Planned"))
                await UpdateEventStatus("Planned");
            return result;
        }

        public async Task<bool> IsCurrentDateBetween()
        {
            bool result = DateTime.Compare(CurrentDate, Event.EventStartDate.Value) >= 0 && DateTime.Compare(DateTime.Now, Event.EventEndDate.Value) <= 0;
            if (result && !Event.Status.Name.Equals("In Progress"))
                await UpdateEventStatus("In Progress");
            return result;
        }

        public async Task<bool> IsCurrentDateAfterEnd()
        {
            bool result = DateTime.Compare(CurrentDate, Event.EventEndDate.Value) > 0;
            if (result && !Event.Status.Name.Equals("Ended"))
                await UpdateEventStatus("Ended");
            return result;
        }

        private async Task UpdateEventStatus(string statusName)
        {
            var eventDTO = await EventRepository.UpdateEventStatus(new EventStatusModel { IdEvent = Event.IdEvent, Status = StatusList.FirstOrDefault(x => x.Name.Equals(statusName)) });
            if (!string.IsNullOrEmpty(eventDTO.Detail))
            {
                SnackBar.Add(eventDTO.Detail, Severity.Error);
            }
            else
            {
                Event.Status = eventDTO.Status;
                StateHasChanged();
                SnackBar.Add($"Status of the event has been changed to: {statusName}", Severity.Info);
            }
        }
    }
}
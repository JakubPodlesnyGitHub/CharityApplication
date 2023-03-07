using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Event
{
    public class UpdateEventCommand : IRequest<BasicEventDTO>
    {
        public int IdEvent { get; set; }
        public string? EventName { get; set; }
        public string? EventGoal { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public int? EventMemeberLimit { get; set; }
        public decimal? OverSale { get; set; }
        public string? EventDesc { get; set; }
        public string? JsonEvent { get; set; }
        public int? IdStatus { get; set; }

        public UpdateEventCommand(
            int idEvent,
            string? eventName,
            string? eventGoal,
            DateTime? eventStartDate,
            DateTime? eventEndDate,
            int? eventMemeberLimit,
            decimal? overSale,
            string? eventDesc,
            int? idStatus)
        {
            IdEvent = idEvent;
            EventName = eventName;
            EventGoal = eventGoal;
            EventStartDate = eventStartDate;
            EventEndDate = eventEndDate;
            EventMemeberLimit = eventMemeberLimit;
            OverSale = overSale;
            EventDesc = eventDesc;
            IdStatus = idStatus;
        }
    }
}
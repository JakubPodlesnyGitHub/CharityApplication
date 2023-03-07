using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Event
{
    public class CreateEventCommand : IRequest<BasicEventDTO>
    {
        public string EventName { get; set; }
        public string EventGoal { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public int EventMemeberLimit { get; set; }
        public decimal OverSale { get; set; }
        public string EventDesc { get; set; }
        public int IdStatus { get; set; }
        public string? Base64dataPicture { get; set; }
        public int IdEventOwner { get; set; }

        public CreateEventCommand(
            string eventName,
            string eventGoal,
            DateTime eventStartDate,
            DateTime eventEndDate,
            int eventMemeberLimit,
            decimal overSale,
            string eventDesc,
            int idStatus,
            string? base64DataPicture)
        {
            EventName = eventName;
            EventGoal = eventGoal;
            EventStartDate = eventStartDate;
            EventEndDate = eventEndDate;
            EventMemeberLimit = eventMemeberLimit;
            OverSale = overSale;
            EventDesc = eventDesc;
            IdStatus = idStatus;
            Base64dataPicture = base64DataPicture;
        }
    }
}
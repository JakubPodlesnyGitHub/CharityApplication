using Application.Dtos.BasicDtos.Responses;
using System.Text.Json.Serialization;

namespace CharityApplication.Client.Model.EventModel
{
    public class EventModel
    {
        public int? IdEvent { get; set; }
        public string? EventName { get; set; }
        public string? EventGoal { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public int? EventMemeberLimit { get; set; }
        public decimal? OverSale { get; set; }
        public string? EventDesc { get; set; }
        public string? JsonEvent { get; set; }
        public string? Base64dataPicture { get; set; }
        public int? IdEventOwner { get; set; }

        [JsonIgnore]
        public BasicStatusDTO Status { get; set; } = null!;

        public int? IdStatus { get => Status.IdStatus; }
    }
}
using Application.Dtos.BasicDtos.Responses;
using System.Text.Json.Serialization;

namespace CharityApplication.Client.Model.EventModel
{
    public class EventStatusModel
    {
        [JsonIgnore]
        public BasicStatusDTO Status { get; set; } = null!;

        public int? IdStatus { get => Status.IdStatus; }
        public int IdEvent { get; set; }
    }
}
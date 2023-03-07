using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicEventDTO : ErrorResponseDTO
    {
        public int IdEvent { get; set; }
        public string EventName { get; set; } = null!;
        public string EventGoal { get; set; } = null!;
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public int EventMemeberLimit { get; set; }
        public decimal OverSale { get; set; }
        public string? EventDesc { get; set; }
        public string? JsonEvent { get; set; }
        public string? Base64dataPicture { get; set; }
        public BasicStatusDTO Status { get; set; } = new BasicStatusDTO();
        public int IdStatus { get => Status?.IdStatus ?? 0; }
        public int IdEventOwner { get; set; }
    }
}
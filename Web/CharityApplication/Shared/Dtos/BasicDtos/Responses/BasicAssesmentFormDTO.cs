using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicAssesmentFormDTO : ErrorResponseDTO
    {
        public int IdAssesmentForm { get; set; }
        public string Mail { get; set; } = null!;
        public int EventRate { get; set; }
        public string Subject { get; set; } = null!;
        public int AppRate { get; set; }
        public string Message { get; set; } = null!;
        public int Event { get; set; }
        public int IdOwner { get; set; }
    }
}
using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicStatusDTO : ErrorResponseDTO
    {
        public int IdStatus { get; set; }
        public string Name { get; set; } = null!;
    }
}
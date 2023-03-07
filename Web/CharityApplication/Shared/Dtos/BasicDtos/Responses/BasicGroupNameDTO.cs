using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicGroupNameDTO : ErrorResponseDTO
    {
        public int IdGroupName { get; set; }
        public string Name { get; set; } = null!;
    }
}
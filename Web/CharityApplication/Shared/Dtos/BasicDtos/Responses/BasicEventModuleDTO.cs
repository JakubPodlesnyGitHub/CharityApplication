using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicEventModuleDTO : ErrorResponseDTO
    {
        public int IdEventModule { get; set; }
        public BasicEventDTO Event { get; set; } = null!;
        public BasicModuleDTO Module { get; set; } = null!;
    }
}
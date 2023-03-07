using CharityApplication.Client.Model.Error;

namespace Application.Dtos.BasicDtos.Responses
{
    public class BasicGroupAccountDTO : ErrorResponseDTO
    {
        public BasicGroupDTO Group { get; set; } = null!;
        public BasicAccountDTO Account { get; set; } = null!;
    }
}
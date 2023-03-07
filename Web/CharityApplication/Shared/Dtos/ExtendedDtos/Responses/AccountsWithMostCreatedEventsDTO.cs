using Application.Dtos.BasicDtos.Responses;

namespace Application.Dtos.ExtendedDtos.Responses
{
    public class AccountsWithMostCreatedEventsDTO : BasicAccountDTO
    {
        public int NumberOfEvents { get; set; }
        public List<BasicEventDTO> CreatedEvents { get; set; }
    }
}
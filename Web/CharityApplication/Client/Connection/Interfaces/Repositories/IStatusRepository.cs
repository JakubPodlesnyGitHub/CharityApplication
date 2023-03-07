using Application.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IStatusRepository
    {
        public Task<List<BasicStatusDTO>> GetEventStatuses();
    }
}
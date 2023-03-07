using Application.Dtos.BasicDtos.Responses;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IGroupNameRepository
    {
        public Task<List<BasicGroupNameDTO>> GetGroupNames();
    }
}
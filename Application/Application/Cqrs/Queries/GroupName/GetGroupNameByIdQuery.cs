using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupName
{
    public class GetGroupNameByIdQuery : IRequest<BasicGroupNameDTO>
    {
        public int Id { get; set; }

        public GetGroupNameByIdQuery(int id)
        {
            Id = id;
        }
    }
}
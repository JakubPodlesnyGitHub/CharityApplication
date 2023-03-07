using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Status
{
    public class GetStatusByIdQuery : IRequest<BasicStatusDTO>
    {
        public int Id { get; set; }

        public GetStatusByIdQuery(int id)
        {
            Id = id;
        }
    }
}
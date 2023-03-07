using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Event
{
    public class GetEventsByAccountIdQuery : IRequest<List<BasicEventDTO>>
    {
        public int IdAccount { get; set; }

        public GetEventsByAccountIdQuery(int idAccount)
        {
            IdAccount = idAccount;
        }
    }
}
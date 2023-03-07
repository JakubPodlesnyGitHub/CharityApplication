using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.EventAccount
{
    public class GetEventAccountsByEventIdQuery : IRequest<List<BasicEventAccountDTO>>
    {
        public int IdEvent { get; set; }

        public GetEventAccountsByEventIdQuery(int idEvent)
        {
            IdEvent = idEvent;
        }
    }
}
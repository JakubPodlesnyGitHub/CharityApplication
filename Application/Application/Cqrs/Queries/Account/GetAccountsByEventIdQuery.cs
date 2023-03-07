using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Account
{
    public class GetAccountsByEventIdQuery : IRequest<List<BasicAccountDTO>>
    {
        public int IdEvent { get; set; }

        public GetAccountsByEventIdQuery(int idEvent)
        {
            IdEvent = idEvent;
        }
    }
}
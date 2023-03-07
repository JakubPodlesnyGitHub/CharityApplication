using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Account
{
    public class GetUnConfirmedAccountsByEventId : IRequest<List<BasicAccountDTO>>
    {
        public int IdEvent { get; set; }

        public GetUnConfirmedAccountsByEventId(int idEvent)
        {
            IdEvent = idEvent;
        }
    }
}
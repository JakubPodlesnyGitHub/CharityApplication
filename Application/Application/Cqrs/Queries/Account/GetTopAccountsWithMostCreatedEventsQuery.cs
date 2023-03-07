using Application.Dtos.ExtendedDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Account
{
    public class GetTopAccountsWithMostCreatedEventsQuery : IRequest<List<AccountsWithMostCreatedEventsDTO>>
    {
        public int NumberOfEvents { get; set; }

        public GetTopAccountsWithMostCreatedEventsQuery(int numberOfEvents)
        {
            NumberOfEvents = numberOfEvents;
        }
    }
}
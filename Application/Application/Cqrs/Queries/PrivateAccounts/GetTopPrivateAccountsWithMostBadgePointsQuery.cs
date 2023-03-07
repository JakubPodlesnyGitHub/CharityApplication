using Application.Dtos.ExtendedDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.PrivateAccounts
{
    public class GetTopPrivateAccountsWithMostBadgePointsQuery : IRequest<List<PrivateAccountWithBadgePointsDTO>>
    {
        public int NumberOfPeople { get; set; }

        public GetTopPrivateAccountsWithMostBadgePointsQuery(int numberOfPeople)
        {
            NumberOfPeople = numberOfPeople;
        }
    }
}
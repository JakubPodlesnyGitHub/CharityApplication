using Application.Dtos.ExtendedDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.CompanyAccount
{
    public class GetTopCompanyAccountsWithMostBadgePointsQuery : IRequest<List<CompanyAccountWithBadgePointsDTO>>
    {
        public int NumberOfCompanyAccounts { get; set; }

        public GetTopCompanyAccountsWithMostBadgePointsQuery(int numberOfCompanyAccounts)
        {
            NumberOfCompanyAccounts = numberOfCompanyAccounts;
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.ComapnyAddress
{
    public class GetCompaniesAddressesQuery : IRequest<List<BasicCompanyAddressDTO>>
    {
    }
}
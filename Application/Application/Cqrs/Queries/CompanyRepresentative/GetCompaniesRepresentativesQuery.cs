using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.CompanyRepresentative
{
    public class GetCompaniesRepresentativesQuery : IRequest<List<BasicCompanyRepresentativeDTO>>
    {
    }
}
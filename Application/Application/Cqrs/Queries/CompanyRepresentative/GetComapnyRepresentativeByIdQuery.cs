using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.CompanyRepresentative
{
    public class GetComapnyRepresentativeByIdQuery : IRequest<BasicCompanyRepresentativeDTO>
    {
        public int Id { get; set; }

        public GetComapnyRepresentativeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.CompanyAccount
{
    public class GetCompanyAccountByIdQuery : IRequest<BasicAccountDTO>
    {
        public int Id { get; set; }

        public GetCompanyAccountByIdQuery(int id)
        {
            Id = id;
        }
    }
}
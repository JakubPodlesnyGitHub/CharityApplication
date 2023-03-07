using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.ComapnyAddress
{
    public class GetCompanyAddressByIdQuery : IRequest<BasicCompanyAddressDTO>
    {
        public int Id { get; set; }

        public GetCompanyAddressByIdQuery(int id)
        {
            Id = id;
        }
    }
}
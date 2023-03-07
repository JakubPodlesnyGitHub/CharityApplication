using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.CompanyAddress
{
    public class DeleteCompanyAddressCommand : IRequest<BasicCompanyAddressDTO>
    {
        public int IdCompanyAddress { get; set; }

        public DeleteCompanyAddressCommand(int idCompanyAddress)
        {
            IdCompanyAddress = idCompanyAddress;
        }
    }
}
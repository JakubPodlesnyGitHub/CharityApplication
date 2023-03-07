using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.CompanyRepresentative
{
    public class DeleteCompanyRepresentativeCommand : IRequest<BasicCompanyRepresentativeDTO>
    {
        public int IdCompanyRepresentative { get; set; }

        public DeleteCompanyRepresentativeCommand(int idCompanyRepresentative)
        {
            IdCompanyRepresentative = idCompanyRepresentative;
        }
    }
}
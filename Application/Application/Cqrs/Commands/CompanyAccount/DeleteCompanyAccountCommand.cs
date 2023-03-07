using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.CompanyAccount
{
    public class DeleteCompanyAccountCommand : IRequest<BasicAccountDTO>
    {
        public int IdAccount { get; set; }

        public DeleteCompanyAccountCommand(int idAccount)
        {
            IdAccount = idAccount;
        }
    }
}
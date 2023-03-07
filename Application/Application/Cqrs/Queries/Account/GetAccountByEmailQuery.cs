using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Account
{
    public class GetAccountByEmailQuery : IRequest<BasicAccountDTO>
    {
        public string Email { get; set; }

        public GetAccountByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
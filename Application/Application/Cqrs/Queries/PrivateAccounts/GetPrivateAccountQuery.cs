using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.PrivateAccounts
{
    public class GetPrivateAccountQuery : IRequest<BasicAccountDTO>
    {
        public int Id { get; set; }

        public GetPrivateAccountQuery(int id)
        {
            Id = id;
        }
    }
}
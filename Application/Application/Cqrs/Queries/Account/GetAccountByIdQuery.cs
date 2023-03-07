using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Account
{
    public class GetAccountByIdQuery : IRequest<BasicAccountDTO>
    {
        public int Id { get; set; }

        public GetAccountByIdQuery(int id)
        {
            Id = id;
        }
    }
}
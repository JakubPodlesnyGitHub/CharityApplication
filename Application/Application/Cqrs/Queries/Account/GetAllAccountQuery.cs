using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Account
{
    public class GetAllAccountQuery : IRequest<List<BasicAccountDTO>>
    {
    }
}
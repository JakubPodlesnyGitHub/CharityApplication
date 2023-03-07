using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Module
{
    public sealed class GetModuleByIdQuery : IRequest<BasicModuleDTO>
    {
        public int IdModule { get; set; }

        public GetModuleByIdQuery(int idModule)
        {
            IdModule = idModule;
        }
    }
}
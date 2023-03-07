using Application.Cqrs.Commands.EventAccount;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class EventAccountProfile : Profile
    {
        public EventAccountProfile()
        {
            CreateMap<AccountEvent, BasicEventAccountDTO>()
                .ForPath(d => d.Event, o => o.MapFrom(s => s.EventNavigation))
                .ForPath(d => d.Account, o => o.MapFrom(s => s.AccountNavigation));
            CreateMap<BasicEventAccountDTO, AccountEvent>();
            CreateMap<CreateEventAccountCommand, AccountEvent>()
                .ForPath(d => d.IdAccount, o => o.MapFrom(s => s.IdAccount));
            CreateMap<UpdateEventAccountCommand, AccountEvent>();
        }
    }
}
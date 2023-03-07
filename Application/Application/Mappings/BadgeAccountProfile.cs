using Application.Cqrs.Commands.BadgeAccount;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class BadgeAccountProfile : Profile
    {
        public BadgeAccountProfile()
        {
            CreateMap<BadgeAccount, BasicBadgeAccountDTO>()
                .ForPath(x => x.Account, o => o.MapFrom(o => o.AccountNavigation))
                .ForPath(x => x.Badge, o => o.MapFrom(o => o.BadgeNavigation));
            CreateMap<BasicBadgeAccountDTO, BadgeAccount>()
                .ForPath(x => x.AccountNavigation, o => o.MapFrom(o => o.Account))
                .ForPath(x => x.BadgeNavigation, o => o.MapFrom(o => o.Badge));
            CreateMap<CreateBadgeAccountCommand, BadgeAccount>();
        }
    }
}
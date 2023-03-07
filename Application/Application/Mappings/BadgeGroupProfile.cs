using Application.Cqrs.Commands.BadgeGroup;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class BadgeGroupProfile : Profile
    {
        public BadgeGroupProfile()
        {
            CreateMap<BadgeGroup, BasicBadgeGroupDTO>()
                .ForPath(x => x.Group, o => o.MapFrom(o => o.GroupNavigation))
                .ForPath(x => x.Badge, o => o.MapFrom(o => o.BadgeNavigation));
            CreateMap<BasicBadgeGroupDTO, BadgeGroup>()
                .ForPath(x => x.GroupNavigation, o => o.MapFrom(o => o.Group))
                .ForPath(x => x.BadgeNavigation, o => o.MapFrom(o => o.Badge));
            CreateMap<CreateBadgeGroupCommand, BadgeGroup>();
        }
    }
}
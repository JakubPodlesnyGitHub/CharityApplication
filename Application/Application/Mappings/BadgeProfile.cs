using Application.Cqrs.Commands.Badge;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class BadgeProfile : Profile
    {
        public BadgeProfile()
        {
            CreateMap<Badge, BasicBadgeDTO>();
            CreateMap<BasicBadgeDTO, Badge>();
            CreateMap<CreateBadgeCommand, Badge>();
            CreateMap<UpdateBadgeCommand, Badge>();
        }
    }
}
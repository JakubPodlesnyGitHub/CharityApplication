using Application.Cqrs.Commands.GroupEvent;
using AutoMapper;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using Domain.Entities;

namespace Application.Mappings
{
    public class GroupEventProfile : Profile
    {
        public GroupEventProfile()
        {
            CreateMap<GroupEvent, BasicGroupEventDTO>()
                .ForPath(d => d.Event, o => o.MapFrom(s => s.EventNavigation))
                .ForPath(d => d.Group, o => o.MapFrom(s => s.GroupNavigation));
            CreateMap<BasicGroupEventDTO, GroupEvent>();
            CreateMap<CreateGroupEventCommand, GroupEvent>();
            CreateMap<UpdateGroupEventCommand, GroupEvent>();
        }
    }
}
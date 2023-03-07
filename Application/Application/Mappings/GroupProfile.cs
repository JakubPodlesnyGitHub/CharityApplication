using Application.Cqrs.Commands.Group;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, BasicGroupDTO>()
                .ForPath(d => d.GroupName,
                o => o.MapFrom(s =>
                new BasicGroupNameDTO
                {
                    IdGroupName = s.GroupNameNavigation.IdGroupName,
                    Name = s.GroupNameNavigation.Name
                }));
            CreateMap<BasicGroupDTO, Group>();
            CreateMap<CreateGroupCommand, Group>()
                .ForPath(d => d.IdGroupName, o => o.MapFrom(s => s.IdGroupName));
            CreateMap<UpdateGroupCommand, Group>();
        }
    }
}
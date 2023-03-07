using Application.Cqrs.Commands.GroupName;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GroupNameProfile : Profile
    {
        public GroupNameProfile()
        {
            CreateMap<GroupName, BasicGroupNameDTO>();
            CreateMap<BasicGroupNameDTO, GroupName>();
            CreateMap<CreateGroupNameCommand, GroupName>();
            CreateMap<UpdateGroupNameCommand, GroupName>();
        }
    }
}
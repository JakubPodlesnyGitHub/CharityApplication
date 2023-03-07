using Application.Cqrs.Commands.GroupAnnouncement;
using AutoMapper;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using Domain.Entities;

namespace Application.Mappings
{
    public class GroupAnnouncementProfile : Profile
    {
        public GroupAnnouncementProfile()
        {
            CreateMap<GroupAnnouncement, BasicGroupAnnouncementDTO>();
            CreateMap<BasicGroupAnnouncementDTO, GroupAnnouncement>();
            CreateMap<CreateGroupAnnouncementCommand, GroupAnnouncement>();
            CreateMap<UpdateGroupAnnouncementCommand, GroupAnnouncement>();
        }
    }
}
using Application.Cqrs.Commands.EventAnnouncement;
using AutoMapper;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using Domain.Entities;

namespace Application.Mappings
{
    public class EventAnnouncementProfile : Profile
    {
        public EventAnnouncementProfile()
        {
            CreateMap<EventAnnouncement, BasicEventAnnouncementDTO>();
            CreateMap<BasicEventAnnouncementDTO, EventAnnouncement>();
            CreateMap<CreateEventAnnouncementCommand, EventAnnouncement>();
            CreateMap<UpdateEventAnnouncementCommand, EventAnnouncement>();
        }
    }
}
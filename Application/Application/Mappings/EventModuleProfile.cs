using Application.Cqrs.Commands.Event;
using Application.Cqrs.Commands.EventModule;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class EventModuleProfile : Profile
    {
        public EventModuleProfile()
        {
            CreateMap<EventModule, BasicEventModuleDTO>();
            CreateMap<BasicEventModuleDTO, EventModule>();
            CreateMap<CreateEventModuleCommand, EventModule>();
            CreateMap<UpdateEventCommand, EventModule>();
        }
    }
}
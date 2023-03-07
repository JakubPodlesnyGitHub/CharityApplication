using Application.Cqrs.Commands.Event;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, BasicEventDTO>()
                .ForPath(d => d.Status, o => o.MapFrom(s => new BasicStatusDTO { IdStatus = s.IdStatus, Name = s.StatusNavigation.Name }))
                .ForPath(d => d.IdStatus, o => o.MapFrom(s => s.IdStatus));
            CreateMap<BasicEventDTO, Event>();
            CreateMap<CreateEventCommand, Event>();
            CreateMap<UpdateEventCommand, Event>();
        }
    }
}
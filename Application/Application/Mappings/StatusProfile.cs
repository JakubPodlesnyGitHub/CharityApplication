using Application.Cqrs.Commands.Status;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, BasicStatusDTO>();
            CreateMap<BasicStatusDTO, Status>();
            CreateMap<CreateStatusCommand, Status>();
            CreateMap<UpdateStatusCommand, Status>();
        }
    }
}
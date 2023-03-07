using Application.Cqrs.Commands.Module;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<Module, BasicModuleDTO>();
            CreateMap<BasicModuleDTO, Module>();
            CreateMap<UpdateModuleCommand, Module>();
        }
    }
}
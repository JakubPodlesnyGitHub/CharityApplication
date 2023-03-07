using Application.Cqrs.Commands.CompanyAddress;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CompanyAddressProfile : Profile
    {
        public CompanyAddressProfile()
        {
            CreateMap<CompanyAddress, BasicCompanyAddressDTO>();
            CreateMap<BasicCompanyAddressDTO, CompanyAddress>();
            CreateMap<CreateCompanyAddressCommand, CompanyAddress>();
            CreateMap<BasicCompanyAddressDTO, CreateCompanyAddressCommand>();
            CreateMap<UpdateCompanyAddressCommand, CompanyAddress>();
        }
    }
}
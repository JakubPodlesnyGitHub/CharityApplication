using Application.Cqrs.Commands.CompanyRepresentative;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ComapnyRepresentativeProfile : Profile
    {
        public ComapnyRepresentativeProfile()
        {
            CreateMap<CompanyRepresentative, BasicCompanyRepresentativeDTO>();
            CreateMap<BasicCompanyRepresentativeDTO, CompanyRepresentative>();
            CreateMap<CreateCompanyRepresentativeCommand, CompanyRepresentative>();
            CreateMap<CreateCompanyRepresentativeCommand, BasicCompanyRepresentativeDTO>();
            CreateMap<BasicCompanyRepresentativeDTO, CreateCompanyRepresentativeCommand>();
            CreateMap<UpdateCompanyRepresentativeCommand, CompanyRepresentative>();
        }
    }
}
using Application.Cqrs.Commands.Auth;
using Application.Cqrs.Commands.CompanyAccount;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CompanyAccountProfile : Profile
    {
        public CompanyAccountProfile()
        {
            CreateMap<CompanyAccount, BasicCompanyAccountDTO>();
            CreateMap<BasicCompanyAccountDTO, CompanyAccount>();

            CreateMap<CreateCompanyAccountCommand, Account>()
                .ForPath(d => d.CompanyAccountNavigation.Name, o => o.MapFrom(s => s.Name))
                .ForPath(d => d.CompanyAccountNavigation.CompanyDesc, o => o.MapFrom(s => s.CompanyDesc))
                .ForPath(d => d.CompanyAccountNavigation.Krs, o => o.MapFrom(s => s.Krs))
                .ForPath(d => d.CompanyAccountNavigation.Nip, o => o.MapFrom(s => s.Nip))
                .ForPath(d => d.CompanyAccountNavigation.BankAccount, o => o.MapFrom(s => s.BankAccount))
                .ForPath(d => d.CompanyAccountNavigation.CompanyWebsiteLink, o => o.MapFrom(s => s.CompanyWebsiteLink))
                .ForPath(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForPath(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone));

            CreateMap<UpdateCompanyAccountCommand, Account>()
                .ForPath(d => d.CompanyAccountNavigation.Name, o => o.MapFrom(s => s.Name))
                .ForPath(d => d.CompanyAccountNavigation.CompanyDesc, o => o.MapFrom(s => s.CompanyDesc))
                .ForPath(d => d.CompanyAccountNavigation.Krs, o => o.MapFrom(s => s.Krs))
                .ForPath(d => d.CompanyAccountNavigation.Nip, o => o.MapFrom(s => s.Nip))
                .ForPath(d => d.CompanyAccountNavigation.IdCompanyRepresentative, o => o.MapFrom(s => s.CompanyRepresentative.IdCompanyRepresentative))
                .ForPath(d => d.CompanyAccountNavigation.IdCompanyAddress, o => o.MapFrom(s => s.CompanyAddress.IdCompanyAddress))
                .ForPath(d => d.CompanyAccountNavigation.BankAccount, o => o.MapFrom(s => s.BankAccount))
                .ForPath(d => d.CompanyAccountNavigation.CompanyWebsiteLink, o => o.MapFrom(s => s.CompanyWebsiteLink));

            CreateMap<UpdateCompanyAccountCommand, CompanyAccount>()
                .ForPath(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForPath(d => d.CompanyDesc, o => o.MapFrom(s => s.CompanyDesc))
                .ForPath(d => d.Krs, o => o.MapFrom(s => s.Krs))
                .ForPath(d => d.Nip, o => o.MapFrom(s => s.Nip))
                .ForPath(d => d.ComapnyRepresentativeNavigation, o => o.MapFrom(s => s.CompanyRepresentative))
                .ForPath(d => d.CompanyAddressNavigation, o => o.MapFrom(s => s.CompanyAddress))
                .ForPath(d => d.BankAccount, o => o.MapFrom(s => s.BankAccount))
                .ForPath(d => d.CompanyWebsiteLink, o => o.MapFrom(s => s.CompanyWebsiteLink));

            CreateMap<CompanyUserRegisterAuthCommand, Account>()
                .ForPath(d => d.CompanyAccountNavigation.Name, o => o.MapFrom(s => s.Name))
                .ForPath(d => d.UserName, o => o.MapFrom(s => s.Email.Split("@", StringSplitOptions.None)[0]))
                .ForPath(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForPath(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone))
                .ForPath(d => d.CompanyAccountNavigation.ComapnyRepresentativeNavigation, o => o.MapFrom(s => s.CompanyRepresentative))
                .ForPath(d => d.CompanyAccountNavigation.CompanyAddressNavigation, o => o.MapFrom(s => s.CompanyAddress));

            CreateMap<CompanyAccount, BasicAccountDTO>()
                .ForPath(d => d.VerificationStatus, o => o.MapFrom(s => s.AccountNavigation.VerificationStatus))
                .ForPath(d => d.Email, o => o.MapFrom(s => s.AccountNavigation.Email))
                .ForPath(d => d.IdAccount, o => o.MapFrom(s => s.AccountNavigation.Id))
                .ForPath(d => d.Phone, o => o.MapFrom(s => s.AccountNavigation.PhoneNumber))
                .ForPath(d => d.GoldSponsorBadge, o => o.MapFrom(s => s.AccountNavigation.GoldSponsorBadge))
                .ForPath(d => d.CompanyAccount.Name, o => o.MapFrom(s => s.Name))
                .ForPath(d => d.CompanyAccount.CompanyDesc, o => o.MapFrom(s => s.CompanyDesc))
                .ForPath(d => d.CompanyAccount.Krs, o => o.MapFrom(s => s.Krs))
                .ForPath(d => d.CompanyAccount.Nip, o => o.MapFrom(s => s.Nip))
                .ForPath(d => d.CompanyAccount.CompanyAddress, o => o.MapFrom(s => s.CompanyAddressNavigation))
                .ForPath(d => d.CompanyAccount.CompanyRepresentative, o => o.MapFrom(s => s.ComapnyRepresentativeNavigation))
                .ForPath(d => d.CompanyAccount.BankAccount, o => o.MapFrom(s => s.BankAccount))
                .ForPath(d => d.CompanyAccount.CompanyWebsiteLink, o => o.MapFrom(s => s.CompanyWebsiteLink));
        }
    }
}
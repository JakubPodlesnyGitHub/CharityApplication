using Application.Cqrs.Commands.Account;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, BasicAccountDTO>()
                .ForPath(d => d.Phone, o => o.MapFrom(s => s.PhoneNumber))
                .ForPath(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForPath(d => d.IdAccount, o => o.MapFrom(s => s.Id))
                .ForPath(d => d.PrivateAccount, o => o.MapFrom(s => s.PrivateAccountNavigation))
                .ForPath(d => d.CompanyAccount, o => o.MapFrom(s => s.CompanyAccountNavigation))
                .ForPath(d => d.CompanyAccount.CompanyRepresentative, o => o.MapFrom(s => s.CompanyAccountNavigation.ComapnyRepresentativeNavigation))
                .ForPath(d => d.CompanyAccount.CompanyAddress, o => o.MapFrom(s => s.CompanyAccountNavigation.CompanyAddressNavigation));

            CreateMap<BasicAccountDTO, Account>()
                .ForPath(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone))
                .ForPath(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForPath(d => d.Id, o => o.MapFrom(s => s.IdAccount))
                .ForPath(d => d.PrivateAccountNavigation, o => o.MapFrom(s => s.PrivateAccount))
                .ForPath(d => d.CompanyAccountNavigation, o => o.MapFrom(s => s.CompanyAccount))
                .ForPath(d => d.CompanyAccountNavigation.ComapnyRepresentativeNavigation, o => o.MapFrom(s => s.CompanyAccount.CompanyRepresentative))
                .ForPath(d => d.CompanyAccountNavigation.CompanyAddressNavigation, o => o.MapFrom(s => s.CompanyAccount.CompanyAddress));

            CreateMap<CreateAccountCommand, Account>()
                .ForPath(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone))
                .ForPath(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForPath(d => d.PasswordHash, o => o.MapFrom(s => s.Password));

            CreateMap<UpdateAccountCommand, Account>()
                .ForPath(d => d.Id, o => o.MapFrom(s => s.IdAccount))
                .ForPath(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForPath(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone))
                .ForPath(d => d.VerificationStatus, o => o.MapFrom(s => s.VerificationStatus))
                .ForPath(d => d.GoldSponsorBadge, o => o.MapFrom(s => s.GoldSponsorBadge))
                .ForPath(d => d.Base64dataPicture, o => o.MapFrom(s => s.Base64dataPicture))
                .ForPath(d => d.PrivateAccountNavigation, o => o.MapFrom(s => s.PrivateAccount))
                .ForPath(d => d.CompanyAccountNavigation, o => o.MapFrom(s => s.CompanyAccount));

            CreateMap<UpdatePasswordAccountCommand, Account>()
                .ForPath(d => d.PasswordHash, o => o.MapFrom(s => s.NewPassword));
        }
    }
}
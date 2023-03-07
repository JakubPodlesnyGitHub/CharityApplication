using Application.Cqrs.Commands.Auth;
using Application.Cqrs.Commands.PrivateAccount;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class PrivateAccountProfile : Profile
    {
        public PrivateAccountProfile()
        {
            CreateMap<PrivateAccount, BasicPrivateAccountDTO>();
            CreateMap<BasicPrivateAccountDTO, PrivateAccount>();

            CreateMap<CreatePrivateAccountCommand, Account>()
                .ForPath(d => d.PrivateAccountNavigation.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForPath(d => d.PrivateAccountNavigation.LastName, o => o.MapFrom(s => s.LastName))
                .ForPath(d => d.PrivateAccountNavigation.BirthDate, o => o.MapFrom(s => s.BirthDate))
                .ForPath(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForPath(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone));

            CreateMap<UpdatePrivateAccountCommand, Account>()
                .ForPath(d => d.PrivateAccountNavigation.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForPath(d => d.PrivateAccountNavigation.LastName, o => o.MapFrom(s => s.LastName))
                .ForPath(d => d.PrivateAccountNavigation.BirthDate, o => o.MapFrom(s => s.BirthDate))
                .ForPath(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForPath(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone))
                .ForPath(d => d.Id, o => o.MapFrom(s => s.IdAccount));

            CreateMap<UpdatePrivateAccountCommand, PrivateAccount>()
                .ForPath(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForPath(d => d.LastName, o => o.MapFrom(s => s.LastName))
                .ForPath(d => d.BirthDate, o => o.MapFrom(s => s.BirthDate));

            CreateMap<PrivateUserRegisterAuthCommand, Account>()
                            .ForPath(d => d.PrivateAccountNavigation.FirstName, o => o.MapFrom(s => s.FirstName))
                            .ForPath(d => d.PrivateAccountNavigation.LastName, o => o.MapFrom(s => s.LastName))
                            .ForPath(d => d.PrivateAccountNavigation.BirthDate, o => o.MapFrom(s => s.BirthDate))
                            .ForPath(d => d.Email, o => o.MapFrom(s => s.Email))
                            .ForPath(d => d.PhoneNumber, o => o.MapFrom(s => s.Phone))
                            .ForPath(d => d.UserName, o => o.MapFrom(s => s.Email.Split("@", StringSplitOptions.None)[0]));

            CreateMap<PrivateAccount, BasicAccountDTO>()
                .ForPath(d => d.VerificationStatus, o => o.MapFrom(s => s.AccountNavigation.VerificationStatus))
                .ForPath(d => d.Email, o => o.MapFrom(s => s.AccountNavigation.Email))
                .ForPath(d => d.Phone, o => o.MapFrom(s => s.AccountNavigation.PhoneNumber))
                .ForPath(d => d.IdAccount, o => o.MapFrom(s => s.AccountNavigation.Id))
                .ForPath(d => d.GoldSponsorBadge, o => o.MapFrom(s => s.AccountNavigation.GoldSponsorBadge))
                .ForPath(d => d.PrivateAccount.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForPath(d => d.PrivateAccount.LastName, o => o.MapFrom(s => s.LastName))
                .ForPath(d => d.PrivateAccount.BirthDate, o => o.MapFrom(s => s.BirthDate));
        }
    }
}
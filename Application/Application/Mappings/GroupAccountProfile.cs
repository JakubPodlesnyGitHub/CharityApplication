using Application.Cqrs.Commands.GroupAccount;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GroupAccountProfile : Profile
    {
        public GroupAccountProfile()
        {
            CreateMap<GroupAccount, BasicGroupAccountDTO>()
                .ForPath(s => s.Group, o => o.MapFrom(s => s.GroupNavigation))
                .ForPath(s => s.Account, o => o.MapFrom(s => s.AccountNavigation));
            CreateMap<BasicGroupAccountDTO, GroupAccount>();
            CreateMap<CreateGroupAccountCommand, GroupAccount>()
                .ForPath(s => s.IdGroup, o => o.MapFrom(s => s.IdGroup))
                .ForPath(s => s.IdAccount, o => o.MapFrom(s => s.IdAccount));
        }
    }
}
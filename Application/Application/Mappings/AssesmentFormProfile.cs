using Application.Cqrs.Commands.AssesmentForm;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class AssesmentFormProfile : Profile
    {
        public AssesmentFormProfile()
        {
            CreateMap<AssesmentForm, BasicAssesmentFormDTO>()
                .ForPath(s => s.Event, o => o.MapFrom(s => s.IdEvent))
                .ForPath(s => s.Mail, o => o.MapFrom(s => s.Mail))
                .ForPath(s => s.IdAssesmentForm, o => o.MapFrom(s => s.Id));
            CreateMap<BasicAssesmentFormDTO, AssesmentForm>()
                .ForPath(s => s.IdEvent, o => o.MapFrom(s => s.Event))
                .ForPath(s => s.Mail, o => o.MapFrom(s => s.Mail))
                .ForPath(s => s.Id, o => o.MapFrom(s => s.IdAssesmentForm));
            CreateMap<CreateAssesmentFormCommand, AssesmentForm>()
                .ForPath(s => s.Mail, o => o.MapFrom(s => s.Mail))
                .ForPath(s => s.IdEvent, o => o.MapFrom(s => s.Event));
            CreateMap<UpdateAssesmentFormCommand, AssesmentForm>()
                .ForPath(s => s.Mail, o => o.MapFrom(s => s.Mail))
                .ForPath(s => s.IdEvent, o => o.MapFrom(s => s.Event))
            .ForPath(s => s.Id, o => o.MapFrom(s => s.IdAssesmentForm));
        }
    }
}
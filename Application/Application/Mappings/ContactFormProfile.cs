using Application.Cqrs.Commands.ContactForm;
using Application.Dtos.BasicDtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ContactFormProfile : Profile
    {
        public ContactFormProfile()
        {
            CreateMap<ContactForm, BasicContactFormDTO>();
            CreateMap<BasicContactFormDTO, ContactForm>();
            CreateMap<CreateContactFormCommand, ContactForm>();
            CreateMap<UpdateContactFormCommand, ContactForm>();
        }
    }
}
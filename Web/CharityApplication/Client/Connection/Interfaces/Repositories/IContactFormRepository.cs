using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Model.ContactForm;

namespace CharityApplication.Client.Connection.Interfaces.Repositories
{
    public interface IContactFormRepository
    {
        public Task<BasicContactFormDTO> CrateContactForm(ContactFormModel contactFrom);
    }
}
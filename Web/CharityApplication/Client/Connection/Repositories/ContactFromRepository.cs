using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Helpers.Http;
using CharityApplication.Client.Model.ContactForm;

namespace CharityApplication.Client.Connection.Repositories
{
    public class ContactFromRepository : IContactFormRepository
    {
        private readonly IHttpService _httpService;

        private readonly string URL = "api/contactForm";

        public ContactFromRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<BasicContactFormDTO> CrateContactForm(ContactFormModel contactFrom)
        {
            var response = await _httpService.Post<ContactFormModel, BasicContactFormDTO>($"{URL}/CreateContactForm", contactFrom);
            return response.Response;
        }
    }
}
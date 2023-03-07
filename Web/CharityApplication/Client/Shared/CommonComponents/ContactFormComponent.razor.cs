using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.ContactForm;
using CharityApplication.Client.Validators.ContactForm;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Shared.CommonComponents
{
    public partial class ContactFormComponent
    {
        private ContactFormModel Model = new ContactFormModel();
        private ContactFormModelValidator Validator = new ContactFormModelValidator();
        private MudForm? Form;

        [Inject]
        public IContactFormRepository ContactFormRepository { get; set; }

        private async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                BasicContactFormDTO contactFormDTO = await ContactFormRepository.CrateContactForm(Model);
                if (!string.IsNullOrEmpty(contactFormDTO.Detail))
                {
                    SnackBar.Add("Your message has been sent", Severity.Success);
                    Form.Reset();
                }
                else
                {
                    SnackBar.Add(contactFormDTO.Detail, Severity.Error);
                }
            }
        }
    }
}
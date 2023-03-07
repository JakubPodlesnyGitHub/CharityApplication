using CharityApplication.Client.Model.ContactForm;
using FluentValidation;

namespace CharityApplication.Client.Validators.ContactForm
{
    public class ContactFormModelValidator : AbstractValidator<ContactFormModel>
    {
        public ContactFormModelValidator()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty().NotNull().WithMessage("First Name is required")
               .MaximumLength(200).WithMessage("Maximum length of email is 200 characters");
            RuleFor(x => x.LastName)
                .NotEmpty().NotNull().WithMessage("Last Name is required")
                .MaximumLength(200).WithMessage("Maximum length of email is 200 characters");
            RuleFor(x => x.Subject)
                .NotEmpty().NotNull().WithMessage("Subject is required");
            RuleFor(x => x.Message)
                .NotEmpty().NotNull().WithMessage("Message is required");
            RuleFor(x => x.Mail)
                .NotEmpty().NotNull().WithMessage("Mail is required")
                .EmailAddress().WithMessage("Valid mail is required")
                .MaximumLength(100).WithMessage("Maximum length of email is 100 characters");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ContactFormModel>.CreateWithOptions((ContactFormModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
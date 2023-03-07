using Application.Cqrs.Commands.ContactForm;
using FluentValidation;

namespace Application.Validation.ContactFormValidation
{
    public sealed class UpdateContactFormCommandValidator : AbstractValidator<UpdateContactFormCommand>
    {
        public UpdateContactFormCommandValidator()
        {
            RuleFor(x => x.IdContactForm)
                .NotEmpty().NotNull().WithMessage("The id of the contact form is required");
            RuleFor(x => x.FirstName)
                .NotEmpty().NotNull().WithMessage("First Name is required")
                .MaximumLength(200).WithMessage("Maximum length of email is 200 characters");
            RuleFor(x => x.LastName)
                .NotEmpty().NotNull().WithMessage("Last Name is required")
                .MaximumLength(200).WithMessage("Maximum length of email is 200 characters");
            RuleFor(x => x.Subject)
                .NotEmpty().NotNull().WithMessage("Subject is required");
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required");
            RuleFor(x => x.Mail)
                .NotEmpty().NotNull().WithMessage("Mail is required")
                .EmailAddress().WithMessage("Valid Mail is required")
                .MaximumLength(100).WithMessage("Maximum length of email is 100 characters");
        }
    }
}
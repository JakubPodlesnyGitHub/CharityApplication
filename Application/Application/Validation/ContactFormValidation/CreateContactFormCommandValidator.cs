using Application.Cqrs.Commands.ContactForm;
using FluentValidation;

namespace Application.Validation.ContactFormValidation
{
    public sealed class CreateContactFormCommandValidator : AbstractValidator<CreateContactFormCommand>
    {
        public CreateContactFormCommandValidator()
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
                .NotEmpty().NotNull().WithMessage("mail is required")
                .EmailAddress().WithMessage("Valid mail is required")
                .MaximumLength(100).WithMessage("Maximum length of email is 100 characters");
        }
    }
}
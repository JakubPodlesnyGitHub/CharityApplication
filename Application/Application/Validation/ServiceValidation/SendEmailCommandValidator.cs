using Application.Dtos.ServiceDtos.Requests;
using FluentValidation;

namespace Application.Validation.ServiceValidation
{
    public class SendEmailCommandValidator : AbstractValidator<EmailRequestDTO>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(x => x.To)
                .NotEmpty().NotNull().WithMessage("Receiver email is required")
                .EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.FirstName)
                .NotEmpty().NotNull().WithMessage("Receiver first name is required")
                .MaximumLength(200).WithMessage("Maximum Length of first name is 200 characters");
            RuleFor(x => x.LastName)
                .NotEmpty().NotNull().WithMessage("Receiver last name is required")
                .MaximumLength(200).WithMessage("Maximum Length of last name is 200 characters");
            RuleFor(x => x.Subject)
                .NotEmpty().NotNull().WithMessage("Subject is required");
            RuleFor(x => x.Message)
               .NotEmpty().NotNull().WithMessage("Message is required");
        }
    }
}
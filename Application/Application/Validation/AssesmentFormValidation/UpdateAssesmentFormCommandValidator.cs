using Application.Cqrs.Commands.AssesmentForm;
using FluentValidation;

namespace Application.Validation.AssesmentFormValidation
{
    public sealed class UpdateAssesmentFormCommandValidator : AbstractValidator<UpdateAssesmentFormCommand>
    {
        public UpdateAssesmentFormCommandValidator()
        {
            RuleFor(x => x.IdAssesmentForm)
                .NotEmpty().NotNull().WithMessage("The id of the assesment form is required");
            RuleFor(x => x.Mail)
                .NotEmpty().NotNull().WithMessage("Email Address is required")
                .MaximumLength(100).WithMessage("Email Address maxium length is 100 Characters")
                .EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.EventRate)
                .NotEmpty().NotNull().WithMessage("Event Rate is required")
                .InclusiveBetween(1, 10).WithMessage("Event Rate is between 1 - 10");
            RuleFor(x => x.Subject)
                .NotEmpty().NotNull().WithMessage("Subject is required");
            RuleFor(x => x.AppRate)
                .NotEmpty().NotNull().WithMessage("App Rate is required")
                .InclusiveBetween(1, 10).WithMessage("App Rate is between 1 - 10");
            RuleFor(x => x.Message)
                .NotEmpty().NotNull().WithMessage("Message is required");
        }
    }
}
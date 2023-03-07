using Application.Cqrs.Commands.Status;
using FluentValidation;

namespace Application.Validation.StatusValidation
{
    public sealed class CreateStatusCommandValidator : AbstractValidator<CreateStatusCommand>
    {
        public CreateStatusCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Status Name is required")
                .MaximumLength(200).WithMessage("Maximum length of status name is required");
        }
    }
}
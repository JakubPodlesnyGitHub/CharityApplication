using Application.Cqrs.Commands.Status;
using FluentValidation;

namespace Application.Validation.StatusValidation
{
    public sealed class UpdateStatusCommandValidator : AbstractValidator<UpdateStatusCommand>
    {
        public UpdateStatusCommandValidator()
        {
            RuleFor(x => x.IdStatus)
                .NotEmpty().NotNull().WithMessage("The id of the status is required");
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Status Name is required")
                .MaximumLength(200).WithMessage("Maximum length of status name is required");
        }
    }
}
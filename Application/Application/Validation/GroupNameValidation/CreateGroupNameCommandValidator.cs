using Application.Cqrs.Commands.GroupName;
using FluentValidation;

namespace Application.Validation.GroupNameValidation
{
    public sealed class CreateGroupNameCommandValidator : AbstractValidator<CreateGroupNameCommand>
    {
        public CreateGroupNameCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name length is maximum 200 characters");
        }
    }
}
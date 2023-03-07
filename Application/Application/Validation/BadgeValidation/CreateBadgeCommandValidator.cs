using Application.Cqrs.Commands.Badge;
using FluentValidation;

namespace Application.Validation.Badge
{
    public sealed class CreateBadgeCommandValidator : AbstractValidator<CreateBadgeCommand>
    {
        public CreateBadgeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Maximum length of name is 200 characters");

            RuleFor(x => x.BadgeGoal)
                .NotEmpty().NotNull().WithMessage("Badge Goal is required");

            RuleFor(x => x.Pointstreshold_User)
                .NotEmpty().NotNull().WithMessage("Points treshold for user is required")
                .GreaterThan(0).WithMessage("Points treshold for user must not be negative");

            RuleFor(x => x.Pointstreshold_Group)
                .NotEmpty().NotNull().WithMessage("Points treshold for group is required")
                .GreaterThan(0).WithMessage("Points treshold for group must not be negative");
        }
    }
}
using Application.Cqrs.Commands.Badge;
using FluentValidation;

namespace Application.Validation.Badge
{
    public sealed class UpdateBadgeCommandValidator : AbstractValidator<UpdateBadgeCommand>
    {
        public UpdateBadgeCommandValidator()
        {
            RuleFor(x => x.IdBadge)
                .NotEmpty().NotNull().WithMessage("The id of the badge is required");

            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Maximum length of name is 200 characters");

            RuleFor(x => x.BadgeGoal)
                .NotEmpty().NotNull().WithMessage("Badge Goal is required");

            RuleFor(x => x.Pointstreshold_User)
                .NotEmpty().NotNull().WithMessage("Pointtreshold_User is required")
                .GreaterThan(0).WithMessage("Pointtreshold_User must not be negative");

            RuleFor(x => x.Pointstreshold_Group)
                .NotEmpty().NotNull().WithMessage("Pointstreshold_Group is required")
                .GreaterThan(0).WithMessage("Pointstreshold_Group must not be negative");
        }
    }
}
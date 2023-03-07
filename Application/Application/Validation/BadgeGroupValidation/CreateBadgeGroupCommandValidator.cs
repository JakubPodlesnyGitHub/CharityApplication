using Application.Cqrs.Commands.BadgeGroup;
using FluentValidation;

namespace Application.Validation.BadgeGroupValidation
{
    public sealed class CreateBadgeGroupCommandValidator : AbstractValidator<CreateBadgeGroupCommand>
    {
        public CreateBadgeGroupCommandValidator()
        {
            RuleFor(x => x.IdGroup).NotEmpty().NotNull().WithMessage("Id of the group is required");
            RuleFor(x => x.IdBadge).NotEmpty().NotNull().WithMessage("Id of the badge is required");
        }
    }
}
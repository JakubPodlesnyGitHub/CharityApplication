using Application.Cqrs.Commands.BadgeAccount;
using FluentValidation;

namespace Application.Validation.BadgeAccountValidation
{
    public sealed class CreateBadgeAccountCommandValidator : AbstractValidator<CreateBadgeAccountCommand>
    {
        public CreateBadgeAccountCommandValidator()
        {
            RuleFor(x => x.IdAccount).NotEmpty().NotNull().WithMessage("Id of the account is required");
            RuleFor(x => x.IdBadge).NotEmpty().NotNull().WithMessage("Id of the badge is required");
        }
    }
}
using Application.Cqrs.Commands.GroupAccount;
using FluentValidation;

namespace Application.Validation.GroupAccountValidation
{
    public sealed class CreateGroupAccountCommandValidator : AbstractValidator<CreateGroupAccountCommand>
    {
        public CreateGroupAccountCommandValidator()
        {
            RuleFor(x => x.IdGroup).NotEmpty().NotNull().WithMessage("Id of the group is required");
            RuleFor(x => x.IdAccount).NotEmpty().NotNull().WithMessage("Id of the account is required");
        }
    }
}
using Domain.Entities;
using FluentValidation;

namespace Application.Validation.GroupAccountValidation
{
    public sealed class UpdateGroupAccountCommandValidator : AbstractValidator<GroupAccount>
    {
        public UpdateGroupAccountCommandValidator()
        {
            RuleFor(x => x.IdGroup).NotEmpty().NotNull().WithMessage("Id of the group is required");
            RuleFor(x => x.IdAccount).NotEmpty().NotNull().WithMessage("Id of the account is required");
        }
    }
}
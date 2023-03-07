using Application.Cqrs.Commands.GroupName;
using FluentValidation;

namespace Application.Validation.GroupNameValidation
{
    public sealed class UpdateGroupNameCommandValidator : AbstractValidator<UpdateGroupNameCommand>
    {
        public UpdateGroupNameCommandValidator()
        {
            RuleFor(x => x.IdGroupName).NotEmpty().NotNull().WithMessage("The id of the group name is required");
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name length is maximum 200 characters");
        }
    }
}
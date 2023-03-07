using Application.Cqrs.Commands.Group;
using FluentValidation;

namespace Application.Validation.GroupValidation
{
    public sealed class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(x => x.IdGroup).NotEmpty().NotNull().WithMessage("The id of the group is required");
            RuleFor(x => x.IdGroupName)
                .NotEmpty().NotNull().WithMessage("Name is required");
            RuleFor(x => x.Description)
                .NotEmpty().NotNull().WithMessage("Description is required");
            RuleFor(x => x.NumberOfParticipants)
                .NotEmpty().NotNull().WithMessage("Number of participants is required")
                .InclusiveBetween(1, 30).WithMessage("Group number of particpants must has value between 1 or 30");
            RuleFor(x => x.GroupType)
                .NotEmpty().NotNull().WithMessage("Group Type is required");
        }
    }
}
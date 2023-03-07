using Application.Cqrs.Commands.Group;
using FluentValidation;

namespace Application.Validation.GroupValidation
{
    public sealed class CreateGroupCommnadValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommnadValidator()
        {
            RuleFor(x => x.IdGroupName)
                .NotNull().WithMessage("Name is required");
            RuleFor(x => x.Description)
                .NotEmpty().NotNull().WithMessage("Description is required");
            RuleFor(x => x.NumberOfParticipants)
                .NotNull().WithMessage("Number of participants is required")
                .InclusiveBetween(1, 30).WithMessage("Group number of particpants must has value between 1 or 30");
            RuleFor(x => x.GroupType)
               .NotNull().WithMessage("Group Type is required");
        }
    }
}
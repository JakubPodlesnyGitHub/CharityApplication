using CharityApplication.Client.Model.Group;
using FluentValidation;

namespace CharityApplication.Client.Validators.Group
{
    public class GroupModelValidator : AbstractValidator<GroupModel>
    {
        public GroupModelValidator()
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

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<GroupModel>.CreateWithOptions((GroupModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
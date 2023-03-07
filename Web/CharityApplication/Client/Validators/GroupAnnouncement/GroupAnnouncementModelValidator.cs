using CharityApplication.Client.Model.GroupAnnouncement;
using FluentValidation;

namespace CharityApplication.Client.Validators.GroupAnnouncement
{
    public class GroupAnnouncementModelValidator : AbstractValidator<GroupAnnouncementModel>
    {
        public GroupAnnouncementModelValidator()
        {
            RuleFor(x => x.Subject)
                .NotEmpty().NotNull().WithMessage("Subject is required");
            RuleFor(x => x.Message)
                .NotEmpty().NotNull().WithMessage("Message is required");
            RuleFor(x => x.IdGroup)
                .NotEmpty().NotNull().WithMessage("Group Reference is required");
            RuleFor(x => x.IdOwner)
                .NotEmpty().NotNull().WithMessage("Id Owner is required");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<GroupAnnouncementModel>.CreateWithOptions((GroupAnnouncementModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
using CharityApplication.Client.Model.EventAnnouncement;
using FluentValidation;

namespace CharityApplication.Client.Validators.EventAnnouncement
{
    public class EventAnnouncementModelValidator : AbstractValidator<EventAnnouncementModel>
    {
        public EventAnnouncementModelValidator()
        {
            RuleFor(x => x.Subject)
                .NotEmpty().NotNull().WithMessage("Subject is required");
            RuleFor(x => x.Message)
                .NotEmpty().NotNull().WithMessage("Message is required");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<EventAnnouncementModel>.CreateWithOptions((EventAnnouncementModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
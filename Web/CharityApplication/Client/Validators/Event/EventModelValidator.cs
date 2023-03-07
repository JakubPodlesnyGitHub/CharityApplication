using CharityApplication.Client.Model.EventModel;
using FluentValidation;

namespace CharityApplication.Client.Validators.Event
{
    public class EventModelValidator : AbstractValidator<EventModel>
    {
        public EventModelValidator()
        {
            RuleFor(x => x.EventName)
                .NotEmpty().NotNull().WithMessage("Event Name is required")
                .MaximumLength(200).WithMessage("Maximum length of event name is 200 characters");
            RuleFor(x => x.EventGoal)
                .NotEmpty().NotNull().WithMessage("Event goal is required");
            RuleFor(x => x.EventStartDate)
                .NotEmpty().NotNull().WithMessage("Event start date is required")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Event start date has to be at least today");
            RuleFor(x => x.EventEndDate)
                .NotEmpty().NotNull().WithMessage("Event end date is required");
            RuleFor(x => x.EventMemeberLimit)
                .NotEmpty().NotNull().WithMessage("Event member limit is required")
                .GreaterThan(0).WithMessage("Event has mimium 1 member");
            RuleFor(x => x.OverSale)
                .NotEmpty().NotNull().WithMessage("Over Sale is required")
                .GreaterThanOrEqualTo(0.4m).WithMessage("Maximum Over Sale is 40 %");
            RuleFor(x => x.EventDesc)
                .NotEmpty().NotNull().WithMessage("Event description is required");
            RuleFor(x => x.IdStatus)
                .NotNull().WithMessage("IdStatus is required");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<EventModel>.CreateWithOptions((EventModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
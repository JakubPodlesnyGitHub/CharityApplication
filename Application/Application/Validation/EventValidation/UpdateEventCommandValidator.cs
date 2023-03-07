using Application.Cqrs.Commands.Event;
using FluentValidation;

namespace Application.Validation.EventValidation
{
    public sealed class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.IdEvent).NotEmpty().NotNull().WithMessage("The id of the event is required");
            RuleFor(x => x.EventName)
                .NotEmpty().NotNull().WithMessage("Event Name is required")
                .MaximumLength(200).WithMessage("Maximum length of event name is 200 characters");
            RuleFor(x => x.EventGoal)
                .NotEmpty().NotNull().WithMessage("Event goal is required");
            RuleFor(x => x.EventStartDate)
                .NotEmpty().NotNull().WithMessage("Event start date is required");
            RuleFor(x => x.EventEndDate)
                .NotEmpty().NotNull().WithMessage("Event end date is required");
            RuleFor(x => x.EventMemeberLimit)
                .NotEmpty().NotNull().WithMessage("Event member limit is required")
                .GreaterThan(0).WithMessage("Event has to have mimium 1 member");
            RuleFor(x => x.OverSale)
                .NotEmpty().NotNull().WithMessage("Over Sale is required")
                .GreaterThanOrEqualTo(0.4m).WithMessage("Maximum Over Sale is 40 %");
            RuleFor(x => x.EventDesc)
                .NotEmpty().NotNull().WithMessage("Event description is required");
            RuleFor(x => x.IdStatus)
                .NotNull().WithMessage("IdStatus is required");
        }
    }
}
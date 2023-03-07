using Application.Cqrs.Commands.EventAnnouncement;
using FluentValidation;

namespace Application.Validation.EventAnnouncementValidation
{
    public sealed class CreateEventAnnouncementCommandValidator : AbstractValidator<CreateEventAnnouncementCommand>
    {
        public CreateEventAnnouncementCommandValidator()
        {
            RuleFor(x => x.Subject)
                .NotEmpty().NotNull().WithMessage("Subject is required");
            RuleFor(x => x.Message)
                .NotEmpty().NotNull().WithMessage("Message is required");
            RuleFor(x => x.IdEvent)
                .NotEmpty().NotNull().WithMessage("Event reference is required");
            RuleFor(x => x.IdOwner)
                .NotEmpty().NotNull().WithMessage("Id owner is required");
        }
    }
}
using Application.Cqrs.Commands.EventAnnouncement;
using FluentValidation;

namespace Application.Validation.EventAnnouncementValidation
{
    public sealed class UpdateEventAnnouncementCommandValidator : AbstractValidator<UpdateEventAnnouncementCommand>
    {
        public UpdateEventAnnouncementCommandValidator()
        {
            RuleFor(x => x.IdEventAnnouncement)
                .NotEmpty().NotNull().WithMessage("Id Event announcement is required");
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
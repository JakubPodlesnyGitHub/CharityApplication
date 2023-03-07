using Application.Cqrs.Commands.GroupAnnouncement;
using FluentValidation;

namespace Application.Validation.GroupAnnouncementValidation
{
    public sealed class CreateGroupAnnouncementCommandValidator : AbstractValidator<CreateGroupAnnouncementCommand>
    {
        public CreateGroupAnnouncementCommandValidator()
        {
            RuleFor(x => x.Subject)
                .NotEmpty().NotNull().WithMessage("Subject is required");
            RuleFor(x => x.Message)
                .NotEmpty().NotNull().WithMessage("Message is required");
            RuleFor(x => x.IdGroup)
                .NotEmpty().NotNull().WithMessage("Group reference is required");
            RuleFor(x => x.IdOwner)
                .NotEmpty().NotNull().WithMessage("Id owner is required");
        }
    }
}
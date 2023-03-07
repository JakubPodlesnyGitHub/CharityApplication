using Application.Cqrs.Commands.GroupAnnouncement;
using FluentValidation;

namespace Application.Validation.GroupAnnouncementValidation
{
    public sealed class UpdateGroupAnnouncementCommandValidator : AbstractValidator<UpdateGroupAnnouncementCommand>
    {
        public UpdateGroupAnnouncementCommandValidator()
        {
            RuleFor(x => x.IdGroupAnnouncement)
               .NotEmpty().NotNull().WithMessage("Id group announcement is required");
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
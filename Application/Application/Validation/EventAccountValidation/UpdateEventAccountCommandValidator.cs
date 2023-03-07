using Application.Cqrs.Commands.EventAccount;
using FluentValidation;

namespace Application.Validation.EventAccountValidation
{
    public sealed class UpdateEventAccountCommandValidator : AbstractValidator<CreateEventAccountCommand>
    {
        public UpdateEventAccountCommandValidator()
        {
            RuleFor(x => x.IdEvent)
                .NotEmpty().NotNull().WithMessage("Id of the event is required");
            RuleFor(x => x.IdAccount).NotEmpty().NotNull().WithMessage("Id of the account is required");
        }
    }
}
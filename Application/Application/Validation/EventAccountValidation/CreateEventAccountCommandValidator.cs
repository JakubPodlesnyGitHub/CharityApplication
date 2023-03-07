using Application.Cqrs.Commands.EventAccount;
using FluentValidation;

namespace Application.Validation.EventAccountValidation
{
    public sealed class CreateEventAccountCommandValidator : AbstractValidator<CreateEventAccountCommand>
    {
        public CreateEventAccountCommandValidator()
        {
            RuleFor(x => x.IdEvent)
                .NotEmpty().NotNull().WithMessage("Id of the event is required")
                .GreaterThan(0).WithMessage("Id of the event should be grater than 0");
            RuleFor(x => x.IdAccount)
                .NotEmpty().NotNull().WithMessage("Id of the account is required")
                .GreaterThan(0).WithMessage("Id of the account should be grater than 0");
        }
    }
}
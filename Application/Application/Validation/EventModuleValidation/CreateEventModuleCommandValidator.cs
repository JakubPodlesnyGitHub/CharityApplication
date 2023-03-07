using Application.Cqrs.Commands.EventModule;
using FluentValidation;

namespace Application.Validation.EventModuleValidation
{
    public sealed class CreateEventModuleCommandValidator : AbstractValidator<CreateEventModuleCommand>
    {
        public CreateEventModuleCommandValidator()
        {
            RuleFor(x => x.IdEvent)
                .NotEmpty().NotNull().WithMessage("Id Event is required");
            RuleFor(x => x.IdModule)
                .NotEmpty().NotNull().WithMessage("Id Module is required");
        }
    }
}
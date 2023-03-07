using Application.Cqrs.Commands.EventModule;
using FluentValidation;

namespace Application.Validation.EventModuleValidation
{
    public sealed class UpdateEventModuleCommandValidator : AbstractValidator<UpdateEventModuleCommand>
    {
        public UpdateEventModuleCommandValidator()
        {
            RuleFor(x => x.IdEventModule)
                .NotEmpty().NotNull().WithMessage("Id EventModule is required");
            RuleFor(x => x.IdEvent)
                .NotEmpty().NotNull().WithMessage("Id Event is required");
            RuleFor(x => x.IdModule)
                .NotEmpty().NotNull().WithMessage("Id Module is required");
        }
    }
}
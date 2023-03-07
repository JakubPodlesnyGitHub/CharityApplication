using Application.Cqrs.Commands.Module;
using FluentValidation;

namespace Application.Validation.ModuleValidation
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator()
        {
            RuleFor(x => x.ModuleName)
                .NotEmpty().NotNull().WithMessage("Module name is required")
                .MaximumLength(200).WithMessage("Maximum length of module name is 200 characters");
            RuleFor(x => x.ModuleDesc)
                .NotEmpty().NotNull().WithMessage("Module description is required");
            //RuleFor(x => x.Properties)
            //    .NotEmpty().NotNull().WithMessage("Module must have at least one property");
            //RuleForEach(x => x.Properties)
            //    .SetValidator(new ModulePropertyValidator());
        }
    }
}
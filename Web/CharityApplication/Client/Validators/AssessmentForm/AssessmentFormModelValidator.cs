using CharityApplication.Client.Model.AssessmentForm;
using FluentValidation;

namespace CharityApplication.Client.Validators.AssessmentForm
{
    public class AssessmentFormModelValidator : AbstractValidator<AssessmentFormModel>
    {
        public AssessmentFormModelValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().NotNull().WithMessage("The message is required");
            RuleFor(x => x.Subject)
                .NotEmpty().NotNull().WithMessage("The subject is required");
            RuleFor(x => x.AppRate)
                .LessThan(0).WithMessage("The app rate cannot be less than 0");
            RuleFor(x => x.EventRate)
                .LessThan(0).WithMessage("The event rate cannot be less than 0");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<AssessmentFormModel>.CreateWithOptions((AssessmentFormModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
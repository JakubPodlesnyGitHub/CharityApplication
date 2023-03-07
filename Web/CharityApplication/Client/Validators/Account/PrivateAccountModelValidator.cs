using Application.Dtos.BasicDtos.Responses;
using FluentValidation;

namespace CharityApplication.Client.Validators.Account
{
    public class PrivateAccountModelValidator : AbstractValidator<BasicPrivateAccountDTO?>
    {
        public PrivateAccountModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().NotNull().WithMessage("First Name is required")
                .MaximumLength(200).WithMessage("Length of first name is maximum 200 characters");
            RuleFor(x => x.LastName)
                .NotEmpty().NotNull().WithMessage("Last Name is required")
                .MaximumLength(200).WithMessage("Length of last name is maximum 200 characters");
            RuleFor(x => x.BirthDate)
                .NotEmpty().NotNull().WithMessage("BirthDate is required")
                .LessThan(DateTime.Now.AddYears(-13)).WithMessage("The person who creates account must be at least 13 years old");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<BasicPrivateAccountDTO>.CreateWithOptions((BasicPrivateAccountDTO)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
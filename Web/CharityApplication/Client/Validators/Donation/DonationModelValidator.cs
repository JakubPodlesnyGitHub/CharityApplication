using CharityApplication.Client.Model.Donation;
using FluentValidation;

namespace CharityApplication.Client.Validators.Donation
{
    public class DonationModelValidator : AbstractValidator<DonationModel>
    {
        public DonationModelValidator()
        {
            RuleFor(x => x.CardNr)
                .NotEmpty().NotNull().WithMessage("Credit card number is required")
                .Length(1, 100)
                .CreditCard().WithMessage("You must provide valid credit card number");
            RuleFor(x => x.ExpirationDate)
                .NotEmpty().NotNull().WithMessage("Credit card expiration date is required")
                .GreaterThan(DateTime.Now).WithMessage("Credit card expiration date must be at least today");
            RuleFor(x => x.CSV)
                .NotEmpty().NotNull().WithMessage("CSV is required")
                .Matches(@"^\d{3}$").WithMessage("CSV Number is invalid.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<DonationModel>.CreateWithOptions((DonationModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
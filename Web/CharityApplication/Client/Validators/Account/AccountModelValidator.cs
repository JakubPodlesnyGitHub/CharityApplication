using CharityApplication.Client.Model.Account;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CharityApplication.Client.Validators.Account
{
    public class AccountModelValidator : AbstractValidator<AccountModel>
    {
        public AccountModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().NotNull().WithMessage("Email Address is required")
                .MaximumLength(100).WithMessage("Email Address maxium length is 100 Characters")
                .EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.Phone)
                .NotEmpty().NotNull().WithMessage("Phone Number is required")
                .MinimumLength(9).WithMessage("Phone Number must be at least 8 character long")
                .MaximumLength(14).WithMessage("Phone Number must not exceed 12 characterlong")
                .Matches(new Regex("^+[1-9]{1}[0-9]{3,14}$")).WithMessage("A valid phone number is required - 9 numbers or international number");

            RuleFor(x => x.CompanyAccount).SetValidator(new CompanyAccountModelValidator());
            RuleFor(x => x.PrivateAccount).SetValidator(new PrivateAccountModelValidator());
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<AccountModel>.CreateWithOptions((AccountModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
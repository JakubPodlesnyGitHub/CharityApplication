using CharityApplication.Client.Model.Auth;
using CharityApplication.Client.Validators.CompanyAddress;
using CharityApplication.Client.Validators.CompanyRepresentative;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CharityApplication.Client.Validators.Auth
{
    public class CompanyAccountAuthModelValidator : AbstractValidator<CompanyAccountAuthModel>
    {
        public CompanyAccountAuthModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().NotNull().WithMessage("Email Address is required")
                .MaximumLength(100).WithMessage("Email Address maxium length is 100 Characters")
                .EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.Password)
               .NotEmpty().NotNull().WithMessage("Your Password cannot be empty")
               .MinimumLength(8).WithMessage("Your password length must be at least 8 characters long")
               .MaximumLength(20).WithMessage("Your password length must not exceed 20 characters long")
               .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
               .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
               .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
               .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
            RuleFor(x => x.RepeatedPassword)
                .NotEmpty().NotNull().WithMessage("Your Password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8 characters long")
                .MaximumLength(20).WithMessage("Your password length must not exceed 20 characters long")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).")
                .Equal(x => x.Password).WithMessage("Repeated Password must be equal to Password");
            RuleFor(x => x.Phone)
                .NotEmpty().NotNull().WithMessage("Phone Number is required")
                .MinimumLength(9).WithMessage("Phone Number must be at least 9 characters long")
                .MaximumLength(14).WithMessage("Phone Number must not exceed 12 characters long")
                .Matches(new Regex("^+[1-9]{1}[0-9]{3,14}$")).WithMessage("A valid phone number is required - 9 numbers or international number");
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Maximum length of First Name is 200 characters");
            RuleFor(x => x.CompanyAddress).SetValidator(new CompanyAddressModelValidator());
            RuleFor(x => x.CompanyRepresentative).SetValidator(new CompanyRepresentativeModelValidator());
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CompanyAccountAuthModel>.CreateWithOptions((CompanyAccountAuthModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
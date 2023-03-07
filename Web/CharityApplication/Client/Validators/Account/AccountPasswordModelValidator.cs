using CharityApplication.Client.Model.Account;
using FluentValidation;

namespace CharityApplication.Client.Validators.Account
{
    public class AccountPasswordModelValidator : AbstractValidator<AccountPasswordModel>
    {
        public AccountPasswordModelValidator()
        {
            RuleFor(x => x.NewPassword)
               .NotEmpty().NotNull().WithMessage("Your Password cannot be empty")
               .MinimumLength(8).WithMessage("Your password length must be at least 8 characters long")
               .MaximumLength(20).WithMessage("Your password length must not exceed 20 characters long")
               .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
               .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
               .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
               .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
            RuleFor(x => x.RepeatedNewPassword)
                .NotEmpty().NotNull().WithMessage("Your Password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8 characters long")
                .MaximumLength(20).WithMessage("Your password length must not exceed 20 characters long")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).")
                .Equal(x => x.NewPassword).WithMessage("Repeated Password must be equal to Password");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<AccountPasswordModel>.CreateWithOptions((AccountPasswordModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
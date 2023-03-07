using Application.Cqrs.Commands.Account;
using FluentValidation;

namespace Application.Validation.AccountValidation
{
    public sealed class UpdatePasswordAccountCommandValidator : AbstractValidator<UpdatePasswordAccountCommand>
    {
        public UpdatePasswordAccountCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().NotNull().WithMessage("The email of the account is required")
                .EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.NewPassword)
                .NotEmpty().NotNull().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8 characters long")
                .MaximumLength(20).WithMessage("Your password length must not exceed 20 characters long")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
            RuleFor(x => x.RepeatedNewPassword)
                .NotEmpty().NotNull().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8 characters long")
                .MaximumLength(20).WithMessage("Your password length must not exceed 20 characters long")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).")
                .Equal(x => x.NewPassword).WithMessage("Repeated Password must be equal to Password");
        }
    }
}
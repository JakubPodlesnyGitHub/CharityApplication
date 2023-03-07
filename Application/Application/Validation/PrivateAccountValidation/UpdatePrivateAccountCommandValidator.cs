using Application.Cqrs.Commands.PrivateAccount;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validation.PrivateAccountValidation
{
    public sealed class UpdatePrivateAccountCommandValidator : AbstractValidator<UpdatePrivateAccountCommand>
    {
        public UpdatePrivateAccountCommandValidator()
        {
            RuleFor(x => x.IdAccount).NotEmpty().NotNull().WithMessage("The id of the private account is required");
            RuleFor(x => x.Email)
               .NotEmpty().NotNull().WithMessage("Email Address is required")
               .MaximumLength(100).WithMessage("Email Address maxium length is 100 Characters")
               .EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.Phone)
                .NotEmpty().NotNull().NotNull().WithMessage("Phone Number is required")
                .MinimumLength(9).WithMessage("Phone Number must be at least 8 character long")
                .MaximumLength(14).WithMessage("Phone Number must not exceed 12 characterlong")
                .Matches(new Regex("^+[1-9]{1}[0-9]{3,14}$")).WithMessage("A valid phone number is required - 9 numbers or international number");
            RuleFor(x => x.FirstName)
                .NotEmpty().NotNull().WithMessage("First Name is required")
                .MaximumLength(200).WithMessage("Length of first name is maximum 200 characters");
            RuleFor(x => x.LastName)
                .NotEmpty().NotNull().WithMessage("Last Name is required")
                .MaximumLength(200).WithMessage("Length of last name is maximum 200 characters");
            RuleFor(x => x.BirthDate)
                .NotEmpty().NotNull().WithMessage("BirthDate is required")
                .IsPersonAtLeast13YerasOld();
        }
    }
}
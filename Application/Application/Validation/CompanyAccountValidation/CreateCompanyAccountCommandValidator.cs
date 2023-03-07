using Application.Cqrs.Commands.CompanyAccount;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validation.CompanyAccountValidation
{
    public sealed class CreateCompanyAccountCommandValidator : AbstractValidator<CreateCompanyAccountCommand>
    {
        public CreateCompanyAccountCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().NotNull().WithMessage("Email address is required")
                .MaximumLength(100).WithMessage("Email address maxium length is 100 Characters")
                .EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.Password)
                .NotEmpty().NotNull().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8 characters long")
                .MaximumLength(20).WithMessage("Your password length must not exceed 20 characters long")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
            RuleFor(x => x.Phone)
                .NotEmpty().NotNull().WithMessage("Phone number is required")
                .MinimumLength(9).WithMessage("Phone number must be at least 8 character long")
                .MaximumLength(14).WithMessage("Phone number must not exceed 12 characterlong")
                .Matches(new Regex("^+[1-9]{1}[0-9]{3,14}$")).WithMessage("A valid phone number is required - 9 numbers or international number");
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Maximum length of First Name is 200 characters");
            RuleFor(x => x.CompanyDesc)
                .NotEmpty().NotNull().WithMessage("Company description is requierd");
            RuleFor(x => x.Krs)
                .Length(10).When(x => !string.IsNullOrEmpty(x.Krs)).WithMessage("Krs should have 10 numbers");
            RuleFor(x => x.Nip)
                .Length(10).When(x => !string.IsNullOrEmpty(x.Nip)).WithMessage("Nip should have 10 numbers")
                .Matches(new Regex("^[0-9]{10}$")).When(x => !string.IsNullOrEmpty(x.Nip)).WithMessage("Nip should have 10 numbers")
                .IsNipValid().When(x => !string.IsNullOrEmpty(x.Nip));
            RuleFor(x => x.CompanyAddress)
                .NotEmpty().NotNull().WithMessage("Id company address is required");
            RuleFor(x => x.CompanyRepresentative)
                .NotEmpty().NotNull().WithMessage("Id company representative is required");
            RuleFor(x => x.BankAccount)
                .Length(26).WithMessage("The bank account nubmer must have 26 numbers").When(x => !string.IsNullOrEmpty(x.BankAccount))
                .Matches(new Regex("^[0-9]{26}$")).WithMessage("The bank acocunt number should have 26 numbers").When(x => !string.IsNullOrEmpty(x.BankAccount));
            RuleFor(x => x.CompanyWebsiteLink)
                .IsWebsiteLinkValid().When(x => !string.IsNullOrEmpty(x.CompanyWebsiteLink));
        }
    }
}
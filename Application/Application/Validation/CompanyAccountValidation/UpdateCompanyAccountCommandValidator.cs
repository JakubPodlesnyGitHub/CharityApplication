using Application.Cqrs.Commands.CompanyAccount;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validation.CompanyAccountValidation
{
    public sealed class UpdateCompanyAccountCommandValidator : AbstractValidator<UpdateCompanyAccountCommand>
    {
        public UpdateCompanyAccountCommandValidator()
        {
            //RuleFor(x => x.IdAccount)
            //    .NotEmpty().NotNull().WithMessage("The id of the account is required");
            //RuleFor(x => x.Email)
            //   .NotEmpty().NotNull().WithMessage("Email address is required")
            //   .MaximumLength(100).WithMessage("Email address maxium length is 100 characters")
            //   .EmailAddress().WithMessage("A valid email is required");
            //RuleFor(x => x.Phone)
            //    .NotEmpty().NotNull().WithMessage("Phone number is required")
            //    .MinimumLength(9).WithMessage("Phone number must be at least 8 character long")
            //    .MaximumLength(14).WithMessage("Phone number must not exceed 12 characterlong")
            //    .Matches(new Regex("^+[1-9]{1}[0-9]{3,14}$")).WithMessage("A valid phone number is required - 9 numbers or international number");
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Maximum length of first name is 200 characters");
            RuleFor(x => x.Krs)
                .Length(10).WithMessage("Krs should have 10 numbers").When(x => !string.IsNullOrEmpty(x.Krs));
            RuleFor(x => x.Nip)
                .NotEmpty().NotNull().WithMessage("Nip is required")
                .Length(10).WithMessage("Nip should have 10 numbers")
                .Matches(new Regex("^[0-9]{10}$")).WithMessage("Nip should have 10 numbers")
                .IsNipValid()
                .When(x => !string.IsNullOrEmpty(x.Krs));
            RuleFor(x => x.CompanyAddress)
                .NotEmpty().NotNull().WithMessage("Company address is required");
            RuleFor(x => x.CompanyRepresentative)
                .NotEmpty().NotNull().WithMessage("Company representative is required");
            RuleFor(x => x.BankAccount)
                .Length(26).WithMessage("The bank account number must have 26 numbers").When(x => !string.IsNullOrEmpty(x.BankAccount))
                .Matches(new Regex("^[0-9]{26}$")).WithMessage("The bank acocunt number should have 26 numbers").When(x => !string.IsNullOrEmpty(x.BankAccount));
            RuleFor(x => x.CompanyWebsiteLink)
                .IsWebsiteLinkValid().When(x => !string.IsNullOrEmpty(x.CompanyWebsiteLink));
        }
    }
}
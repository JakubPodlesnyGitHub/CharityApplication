using CharityApplication.Client.Model.CompanyRepresentative;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CharityApplication.Client.Validators.CompanyRepresentative
{
    public class CompanyRepresentativeModelValidator : AbstractValidator<CompanyRepresentativeModel>
    {
        public CompanyRepresentativeModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().NotNull().WithMessage("First name is required")
                .MaximumLength(200).WithMessage("Maximum length of First Name is 200 characters");
            RuleFor(x => x.LastName)
                .NotEmpty().NotNull().WithMessage("First name is required")
                .MaximumLength(200).WithMessage("Maximum length of last name is 200 characters");
            RuleFor(x => x.RepresentativeMail)
                .NotEmpty().NotNull().WithMessage("Representative mail is required")
                .MaximumLength(100).WithMessage("Maximum length of representative mail is 100 characters")
                .EmailAddress().WithMessage("A email must be valid");
            RuleFor(x => x.RepresentativePhone)
                .NotEmpty().NotNull().WithMessage("Phone number is required")
                .MinimumLength(9).WithMessage("Phone number must be at least 8 character long")
                .MaximumLength(14).WithMessage("Phone number must not exceed 12 characterlong")
                .Matches(new Regex("^+[1-9]{1}[0-9]{3,14}$")).WithMessage("A valid phone number is required - 9 numbers or international number");
        }
    }
}
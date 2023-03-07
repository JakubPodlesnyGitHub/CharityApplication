using Application.Dtos.BasicDtos.Responses;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CharityApplication.Client.Validators.Account
{
    public class CompanyAccountModelValidator : AbstractValidator<BasicCompanyAccountDTO?>
    {
        public CompanyAccountModelValidator()
        {
            RuleFor(x => x.Name)
                 .NotEmpty().NotNull().WithMessage("Name is required")
                 .MaximumLength(200).WithMessage("Maximum length of First Name is 200 characters");
            RuleFor(x => x.CompanyDesc)
                .NotEmpty().NotNull().WithMessage("Company description is requierd");
            RuleFor(x => x.Krs)
                .Length(10).When(x => !string.IsNullOrEmpty(x.Krs)).WithMessage("Krs should have 10 numbers");
            RuleFor(x => x.Nip)
                .Length(10).When(x => !string.IsNullOrEmpty(x.Nip)).WithMessage("Nip should have 10 numbers")
                .Matches(new Regex("^[0-9]{10}$")).When(x => !string.IsNullOrEmpty(x.Nip)).WithMessage("Nip should have 10 numbers");
            RuleFor(x => x.CompanyAddress)
                .NotEmpty().NotNull().WithMessage("Id company address is required");
            RuleFor(x => x.CompanyRepresentative)
                .NotEmpty().NotNull().WithMessage("Id company representative is required");
            RuleFor(x => x.BankAccount)
                .Length(26).WithMessage("The bank account nubmer must have 26 numbers").When(x => !string.IsNullOrEmpty(x.BankAccount))
                .Matches(new Regex("^[0-9]{26}$")).WithMessage("The bank acocunt number should have 26 numbers").When(x => !string.IsNullOrEmpty(x.BankAccount));
            RuleFor(x => x.CompanyWebsiteLink)
                .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.CompanyWebsiteLink));

            RuleFor(x => x.CompanyAddress.Street)
               .NotEmpty().NotNull().WithMessage("Street is required")
               .MaximumLength(200).WithMessage("Maximum length of street is 200 characters");
            RuleFor(x => x.CompanyAddress.BuildingNumber)
                .NotEmpty().NotNull().WithMessage("Building number is required")
                .GreaterThan(0).WithMessage("Building number must have positive number");
            RuleFor(x => x.CompanyAddress.ZipCode)
                .NotEmpty().NotNull().WithMessage("Zip code is required")
                .NotEmpty().WithMessage("Maximum length of zip code is 200 characters")
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Zip code is invalid.");
            RuleFor(x => x.CompanyAddress.City)
                .NotEmpty().NotNull().WithMessage("City is required")
                .MaximumLength(200).WithMessage("Maximum length of city is 200 characters");
            RuleFor(x => x.CompanyAddress.Province)
                .NotEmpty().NotNull().WithMessage("Province is required")
                .MaximumLength(200).WithMessage("Maximum length of province is 200 characters");
            RuleFor(x => x.CompanyAddress.Country)
                .NotEmpty().NotNull().WithMessage("Country is required")
                .MaximumLength(200).WithMessage("Maximum length of country is 200 characters");

            RuleFor(x => x.CompanyRepresentative.FirstName)
                .NotEmpty().NotNull().WithMessage("First name is required")
                .MaximumLength(200).WithMessage("Maximum length of First Name is 200 characters");
            RuleFor(x => x.CompanyRepresentative.LastName)
                .NotEmpty().NotNull().WithMessage("First name is required")
                .MaximumLength(200).WithMessage("Maximum length of last name is 200 characters");
            RuleFor(x => x.CompanyRepresentative.RepresentativeMail)
                .NotEmpty().NotNull().WithMessage("Representative mail is required")
                .MaximumLength(100).WithMessage("Maximum length of representative mail is 100 characters")
                .EmailAddress().WithMessage("A email must be valid");
            RuleFor(x => x.CompanyRepresentative.RepresentativePhone)
                .NotEmpty().NotNull().WithMessage("Phone number is required")
                .MinimumLength(9).WithMessage("Phone number must be at least 8 character long")
                .MaximumLength(14).WithMessage("Phone number must not exceed 12 characterlong")
                .Matches(new Regex("^+[1-9]{1}[0-9]{3,14}$")).WithMessage("A valid phone number is required - 9 numbers or international number");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<BasicCompanyAccountDTO>.CreateWithOptions((BasicCompanyAccountDTO)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
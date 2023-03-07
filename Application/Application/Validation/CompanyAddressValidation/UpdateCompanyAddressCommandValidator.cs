﻿using Application.Cqrs.Commands.CompanyAddress;
using FluentValidation;

namespace Application.Validation.CompanyAddressValidation
{
    public sealed class UpdateCompanyAddressCommandValidator : AbstractValidator<UpdateCompanyAddressCommand>
    {
        public UpdateCompanyAddressCommandValidator()
        {
            RuleFor(x => x.IdCompanyAddress)
                .NotEmpty().NotNull().WithMessage("The id of the company address is required");
            RuleFor(x => x.Street)
                .NotEmpty().NotNull().WithMessage("Street is required")
                .MaximumLength(200).WithMessage("Maximum length of street is 200 characters");
            RuleFor(x => x.BuildingNumber)
                .NotEmpty().NotNull().WithMessage("Street is required")
                .GreaterThan(0).WithMessage("Building number must have positive number");
            RuleFor(x => x.ZipCode)
                .NotEmpty().NotNull().WithMessage("Zip code is required")
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Zip code is invalid.");
            RuleFor(x => x.City)
                .NotEmpty().NotNull().WithMessage("City is required")
                .MaximumLength(200).WithMessage("Maximum length of city is 200 characters");
            RuleFor(x => x.Province)
                .NotEmpty().NotNull().WithMessage("Province is required")
                .MaximumLength(200).WithMessage("Maximum length of province is 200 characters");
            RuleFor(x => x.Country)
                .NotEmpty().NotNull().WithMessage("Country is required")
                .MaximumLength(200).WithMessage("Maximum length of country is 200 characters");
        }
    }
}
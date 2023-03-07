using Application.Cqrs.Commands.CompanyAddress;
using Application.Cqrs.Commands.CompanyRepresentative;
using Application.Dtos.ServiceDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Auth
{
    public sealed class CompanyUserRegisterAuthCommand : IRequest<AuthResponseDTO>
    {
        public string Name { get; set; } = null!;
        public CreateCompanyAddressCommand CompanyAddress { get; set; } = null!;
        public CreateCompanyRepresentativeCommand CompanyRepresentative { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RepeatedPassword { get; set; } = null!;

        public CompanyUserRegisterAuthCommand(string name, CreateCompanyAddressCommand companyAddress, CreateCompanyRepresentativeCommand companyRepresentative, string email, string phone, string password, string repeatedPassword)
        {
            Name = name;
            CompanyAddress = companyAddress;
            CompanyRepresentative = companyRepresentative;
            Email = email;
            Phone = phone;
            Password = password;
            RepeatedPassword = repeatedPassword;
        }
    }
}
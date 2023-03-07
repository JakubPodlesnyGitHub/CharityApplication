using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.CompanyRepresentative
{
    public class CreateCompanyRepresentativeCommand : IRequest<BasicCompanyRepresentativeDTO>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string RepresentativeMail { get; set; } = null!;
        public string? RepresentativePhone { get; set; }

        public CreateCompanyRepresentativeCommand(string firstName, string lastName, string representativeMail, string? representativePhone)
        {
            FirstName = firstName;
            LastName = lastName;
            RepresentativeMail = representativeMail;
            RepresentativePhone = representativePhone;
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.CompanyRepresentative
{
    public class UpdateCompanyRepresentativeCommand : IRequest<BasicCompanyRepresentativeDTO>
    {
        public int IdCompanyRepresentative { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string RepresentativeMail { get; set; } = null!;
        public string RepresentativePhone { get; set; } = null!;

        public UpdateCompanyRepresentativeCommand(int idCompanyRepresentative, string firstName, string lastName, string representativeMail, string representativePhone)
        {
            IdCompanyRepresentative = idCompanyRepresentative;
            FirstName = firstName;
            LastName = lastName;
            RepresentativeMail = representativeMail;
            RepresentativePhone = representativePhone;
        }
    }
}
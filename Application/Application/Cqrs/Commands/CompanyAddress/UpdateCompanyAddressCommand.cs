using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.CompanyAddress
{
    public class UpdateCompanyAddressCommand : IRequest<BasicCompanyAddressDTO>
    {
        public int IdCompanyAddress { get; set; }
        public string Street { get; set; } = null!;
        public int BuildingNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string Country { get; set; } = null!;

        public UpdateCompanyAddressCommand(
            int idCompanyAddress,
            string street,
            int buildingNumber,
            int? apartmentNumber,
            string zipCode,
            string city,
            string province,
            string country)
        {
            IdCompanyAddress = idCompanyAddress;
            Street = street;
            BuildingNumber = buildingNumber;
            ApartmentNumber = apartmentNumber;
            ZipCode = zipCode;
            City = city;
            Province = province;
            Country = country;
        }
    }
}
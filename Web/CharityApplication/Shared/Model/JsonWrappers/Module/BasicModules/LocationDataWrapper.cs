using System.Text.Json.Serialization;

namespace CharityApplication.Shared.Model.JsonWrappers.Module.BasicModules
{
    public class LocationDataWrapper
    {
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public int? AparatmentNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? StreetDestination { get; set; }
        public int? BuildingNumberDestination { get; set; }
        public int? AparatmentNumberDestination { get; set; }
        public string? ZipCodeDestination { get; set; }
        public string? CityDestination { get; set; }
        public string? CountryDestination { get; set; }

        [JsonIgnore]
        public string OriginAddress { get => !string.IsNullOrEmpty(Street) ? $"{Street} {BuildingNumber} {AparatmentNumber},{ZipCode} {City}" : string.Empty; }

        [JsonIgnore]
        public string DestinationAddress { get => !string.IsNullOrEmpty(StreetDestination) ? $"{StreetDestination} {BuildingNumberDestination} {AparatmentNumberDestination},{ZipCodeDestination} {CityDestination}" : string.Empty; }
    }
}
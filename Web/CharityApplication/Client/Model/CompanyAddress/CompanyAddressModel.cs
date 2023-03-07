﻿namespace CharityApplication.Client.Model.CompanyAddress
{
    public class CompanyAddressModel
    {
        public int? IdCompanyAddress { get; set; }
        public string Street { get; set; } = null!;
        public int BuildingNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
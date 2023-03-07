using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class CompanyAddressEntityTypeConfig : IEntityTypeConfiguration<CompanyAddress>
    {
        public void Configure(EntityTypeBuilder<CompanyAddress> builder)
        {
            builder.ToTable(nameof(CompanyAddress));

            builder.HasKey(c => c.IdCompanyAddress);
            builder.Property(c => c.IdCompanyAddress).IsRequired().ValueGeneratedOnAdd();

            builder.Property(c => c.Street).IsRequired().HasMaxLength(200);
            builder.Property(c => c.BuildingNumber).IsRequired();
            builder.Property(c => c.ZipCode).IsRequired().HasMaxLength(200);
            builder.Property(c => c.City).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Province).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Country).IsRequired().HasMaxLength(200);
        }
    }
}
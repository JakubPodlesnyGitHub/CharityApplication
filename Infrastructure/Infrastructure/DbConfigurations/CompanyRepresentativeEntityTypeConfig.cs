using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class CompanyRepresentativeEntityTypeConfig : IEntityTypeConfiguration<CompanyRepresentative>
    {
        public void Configure(EntityTypeBuilder<CompanyRepresentative> builder)
        {
            builder.ToTable(nameof(CompanyRepresentative));

            builder.HasKey(c => c.IdCompanyRepresentative);
            builder.Property(c => c.IdCompanyRepresentative).IsRequired().ValueGeneratedOnAdd();

            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(200);

            builder.Property(c => c.RepresentativePhone).HasMaxLength(30);
            builder.HasIndex(c => c.RepresentativePhone).IsUnique();

            builder.Property(c => c.RepresentativeMail).IsRequired().HasMaxLength(100);
            builder.HasIndex(c => c.RepresentativeMail).IsUnique();

        }
    }
}
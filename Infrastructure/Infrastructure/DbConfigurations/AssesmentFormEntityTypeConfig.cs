using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class AssesmentFormEntityTypeConfig : IEntityTypeConfiguration<AssesmentForm>
    {
        public void Configure(EntityTypeBuilder<AssesmentForm> builder)
        {
            builder.ToTable(nameof(AssesmentForm));

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(a => a.Mail).IsRequired();
            builder.HasIndex(a => a.Mail).IsUnique();

            builder.Property(a => a.EventRate).IsRequired();
            builder.Property(a => a.Subject).IsRequired();
            builder.Property(a => a.AppRate).IsRequired();
            builder.Property(a => a.Message).IsRequired();
        }
    }
}
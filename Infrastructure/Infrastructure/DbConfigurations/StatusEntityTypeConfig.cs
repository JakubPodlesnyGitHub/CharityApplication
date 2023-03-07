using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class StatusEntityTypeConfig : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable(nameof(Status));

            builder.HasKey(s => s.IdStatus);
            builder.Property(s => s.IdStatus).IsRequired().ValueGeneratedOnAdd();

            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.HasIndex(s => s.Name).IsUnique();

            builder.HasData(new Status { IdStatus = 1, Name = "Planned" }, new Status { IdStatus = 2, Name = "In Progress" }, new Status { IdStatus = 3, Name = "Ended" });
        }
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class PrivateAccountEntityTypeConfig : IEntityTypeConfiguration<PrivateAccount>
    {
        public void Configure(EntityTypeBuilder<PrivateAccount> builder)
        {
            builder.ToTable(nameof(PrivateAccount));

            builder.HasKey(p => p.IdAccount).HasName("IdPrivateAccount");
            builder.Property(p => p.IdAccount).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.BirthDate).HasColumnType("date");

            builder.Navigation(p => p.AccountNavigation).AutoInclude();
        }
    }
}
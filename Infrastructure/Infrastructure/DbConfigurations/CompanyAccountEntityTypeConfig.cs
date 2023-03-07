using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class CompanyAccountEntityTypeConfig : IEntityTypeConfiguration<CompanyAccount>
    {
        public void Configure(EntityTypeBuilder<CompanyAccount> builder)
        {
            builder.ToTable(nameof(CompanyAccount));

            builder.HasKey(e => e.IdAccount).HasName("IdCompanyAccount");
            builder.Property(e => e.IdAccount).IsRequired().ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Krs).HasMaxLength(10).HasColumnName("KRS");
            builder.Property(e => e.Nip).HasMaxLength(10).HasColumnName("NIP");
            builder.Property(e => e.BankAccount).HasMaxLength(26);
            builder.Property(e => e.CompanyWebsiteLink).HasMaxLength(2000);

            builder.Navigation(a => a.AccountNavigation).AutoInclude();
            builder.Navigation(a => a.CompanyAddressNavigation).AutoInclude();
            builder.Navigation(a => a.ComapnyRepresentativeNavigation).AutoInclude();

            builder.HasOne(p => p.CompanyAddressNavigation)
                .WithMany(c => c.CompanyAccountsCollection)
                .HasForeignKey(p => p.IdCompanyAddress)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ComapnyRepresentativeNavigation)
                .WithMany(c => c.CompanyAccountsCollection)
                .HasForeignKey(p => p.IdCompanyRepresentative)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
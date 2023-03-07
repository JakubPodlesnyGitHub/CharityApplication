using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class AccountEntityTypeConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(Account));
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Email).IsRequired();
            builder.HasIndex(a => a.Email).IsUnique();
            builder.Property(a => a.VerificationStatus).HasDefaultValue(0).IsRequired();
            builder.Property(a => a.GoldSponsorBadge).HasDefaultValue(0).IsRequired();
            builder.Property(a => a.Points).IsRequired().HasDefaultValue(0);
            builder.HasIndex(a => a.PhoneNumber).IsUnique();

            builder.Navigation(a => a.PrivateAccountNavigation).AutoInclude();
            builder.Navigation(a => a.CompanyAccountNavigation).AutoInclude();

            builder.HasOne(p => p.PrivateAccountNavigation)
                .WithOne(a => a.AccountNavigation)
                .HasForeignKey<PrivateAccount>(p => p.IdAccount)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.CompanyAccountNavigation)
                .WithOne(c => c.AccountNavigation)
                .HasForeignKey<CompanyAccount>(p => p.IdAccount)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.AccountEventCollection)
                .WithOne(ae => ae.AccountNavigation)
                .HasForeignKey(ae => ae.IdAccount)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.AccountGroupCollection)
                .WithOne(g => g.AccountNavigation)
                .HasForeignKey(b => b.IdAccount)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
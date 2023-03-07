using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class BadgeAccountEntityTypeConfig : IEntityTypeConfiguration<BadgeAccount>
    {
        public void Configure(EntityTypeBuilder<BadgeAccount> builder)
        {
            builder.ToTable(nameof(BadgeAccount));

            builder.HasKey(b => new { b.IdBadge, b.IdAccount });

            builder.Property(b => b.DateOfReceivingTheBadge).HasColumnType("date").HasDefaultValue(DateTime.Now);

            builder.Navigation(b => b.BadgeNavigation).AutoInclude();
            builder.Navigation(b => b.AccountNavigation).AutoInclude();

            builder.HasOne(b => b.AccountNavigation)
                .WithMany(b => b.AccountBadgeCollection)
                .HasForeignKey(b => b.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(b => b.BadgeNavigation)
                .WithMany(b => b.BadgeAccounts)
                .HasForeignKey(b => b.IdBadge)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
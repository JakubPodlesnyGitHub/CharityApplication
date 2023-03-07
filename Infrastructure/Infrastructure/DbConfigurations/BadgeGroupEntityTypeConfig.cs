using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class BadgeGroupEntityTypeConfig : IEntityTypeConfiguration<BadgeGroup>
    {
        public void Configure(EntityTypeBuilder<BadgeGroup> builder)
        {
            builder.ToTable(nameof(BadgeGroup));

            builder.HasKey(g => new { g.IdGroup, g.IdBadge });

            builder.Property(g => g.DateOfReceivingTheBadge).HasColumnType("date").HasDefaultValue(DateTime.Now);

            builder.Navigation(g => g.BadgeNavigation).AutoInclude();
            builder.Navigation(g => g.GroupNavigation).AutoInclude();

            builder.HasOne(g => g.BadgeNavigation)
                .WithMany(g => g.BadgeGroups)
                .HasForeignKey(b => b.IdBadge)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(g => g.GroupNavigation)
                .WithMany(g => g.BadgeGroups)
                .HasForeignKey(b => b.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
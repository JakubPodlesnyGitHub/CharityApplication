using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class GroupEntityTypeConfig : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(nameof(Group));

            builder.HasKey(g => g.IdGroup);
            builder.Property(g => g.IdGroup).IsRequired().ValueGeneratedOnAdd();

            builder.Property(g => g.IdGroupName).IsRequired();
            builder.Property(g => g.NumberOfParticipants).IsRequired();
            builder.Property(g => g.CreationGroupDate).HasDefaultValue(DateTime.Now).IsRequired();
            builder.Property(g => g.Description).IsRequired();
            builder.Property(g => g.IdGroupOwner).IsRequired();
            builder.Property(g => g.Points).IsRequired().HasDefaultValue(0);
            builder.Property(g => g.GroupType).IsRequired().HasDefaultValue(false);

            builder.Navigation(a => a.GroupNameNavigation).AutoInclude();

            builder.HasOne(g => g.GroupNameNavigation)
                .WithMany(g => g.Groups)
                .HasForeignKey(g => g.IdGroupName)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(e => e.GroupEvents)
                .WithOne(ge => ge.GroupNavigation)
                .HasForeignKey(ge => ge.IdGroup)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.GroupAccounts)
                .WithOne(g => g.GroupNavigation)
                .HasForeignKey(b => b.IdGroup)
                .OnDelete(DeleteBehavior.Cascade);

            /*builder.HasMany(g => g.GroupAnnouncements)
                .WithOne(g => g.GroupNavigation)
                .HasForeignKey(g => g.IdGroupAnnouncement)
                .OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}
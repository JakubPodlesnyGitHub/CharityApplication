using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class GroupAnnouncementEntityTypeConfig : IEntityTypeConfiguration<GroupAnnouncement>
    {
        public void Configure(EntityTypeBuilder<GroupAnnouncement> builder)
        {
            builder.ToTable(nameof(GroupAnnouncement));

            builder.HasKey(g => g.IdGroupAnnouncement);
            builder.Property(g => g.IdGroupAnnouncement).IsRequired().ValueGeneratedOnAdd();

            builder.Property(g => g.Subject).IsRequired();
            builder.Property(g => g.Message).IsRequired();
            builder.Property(g => g.CreationDate).IsRequired().HasColumnType("date").HasDefaultValue(DateTime.Now);
        }
    }
}
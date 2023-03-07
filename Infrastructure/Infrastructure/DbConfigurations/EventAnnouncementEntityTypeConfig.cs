using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class EventAnnouncementEntityTypeConfig : IEntityTypeConfiguration<EventAnnouncement>
    {
        public void Configure(EntityTypeBuilder<EventAnnouncement> builder)
        {
            builder.ToTable(nameof(EventAnnouncement));

            builder.HasKey(e => e.IdEventAnnouncement);
            builder.Property(e => e.IdEventAnnouncement).IsRequired().ValueGeneratedOnAdd();

            builder.Property(e => e.Subject).IsRequired();
            builder.Property(e => e.Message).IsRequired();
            builder.Property(e => e.CreationDate).IsRequired().HasColumnType("date").HasDefaultValue(DateTime.Now);
        }
    }
}
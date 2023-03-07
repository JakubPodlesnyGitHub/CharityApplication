using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class EventEntityTypeConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable(nameof(Event));

            builder.HasKey(e => e.IdEvent);
            builder.Property(e => e.IdEvent).IsRequired().ValueGeneratedOnAdd();

            builder.Property(a => a.EventName).IsRequired();
            builder.Property(a => a.EventGoal).IsRequired();
            builder.Property(a => a.EventStartDate).IsRequired();
            builder.Property(a => a.EventEndDate).IsRequired();
            builder.Property(a => a.EventMemeberLimit).IsRequired();
            builder.Property(a => a.OverSale).IsRequired().HasColumnType("decimal");
            builder.Property(a => a.EventDesc).IsRequired();
            builder.Property(a => a.IdEventOwner).IsRequired();
            builder.Property(a => a.IdStatus).IsRequired().HasDefaultValue(1);

            builder.Navigation(a => a.StatusNavigation).AutoInclude();

            builder.HasOne(s => s.StatusNavigation)
                .WithMany(e => e.Events)
                .HasForeignKey(s => s.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(e => e.EventModules)
                .WithOne(em => em.EventNavigation)
                .HasForeignKey(em => em.IdEvent)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.AccountEvents)
                .WithOne(ae => ae.EventNavigation)
                .HasForeignKey(ae => ae.IdEvent)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.GroupEvents)
                .WithOne(ge => ge.EventNavigation)
                .HasForeignKey(ge => ge.IdEvent)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
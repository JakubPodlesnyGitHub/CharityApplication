using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    public class GroupEventEntityTypeConfig : IEntityTypeConfiguration<GroupEvent>
    {
        public void Configure(EntityTypeBuilder<GroupEvent> builder)
        {
            builder.ToTable(nameof(GroupEvent));

            builder.HasKey(ge => new { ge.IdGroup, ge.IdEvent });

            builder.Property(ae => ae.IfParticipantEvent).IsRequired().HasDefaultValue(false);
            builder.Property(ae => ae.GroupEventCreation).HasColumnType("date").HasDefaultValue(DateTime.Now);

            builder.Navigation(ae => ae.EventNavigation).AutoInclude();
            builder.Navigation(ae => ae.GroupNavigation).AutoInclude();
        }
    }
}
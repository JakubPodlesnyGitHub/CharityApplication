using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class EventModuleEntityTypeConfig : IEntityTypeConfiguration<EventModule>
    {
        public void Configure(EntityTypeBuilder<EventModule> builder)
        {
            builder.ToTable(nameof(EventModule));

            builder.HasKey(em => em.IdEventModule);
            builder.Property(em => em.IdEventModule).IsRequired().ValueGeneratedOnAdd();

            builder.Navigation(em => em.EventNavigation).AutoInclude();
            builder.Navigation(em => em.ModuleNavigation).AutoInclude();
        }
    }
}
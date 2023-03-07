using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class AccountEventEntityTypeConfig : IEntityTypeConfiguration<AccountEvent>
    {
        public void Configure(EntityTypeBuilder<AccountEvent> builder)
        {
            builder.ToTable(nameof(AccountEvent));

            builder.HasKey(ae => new { ae.IdAccount, ae.IdEvent });
            builder.Property(ae => ae.IfPartcipantPresent).IsRequired().HasDefaultValue(false);
            builder.Property(ae => ae.SubsidyAmount).IsRequired().HasDefaultValue(0);
            builder.Property(ae => ae.EventCreation).HasColumnType("date").HasDefaultValue(DateTime.Now);

            builder.Navigation(ae => ae.EventNavigation).AutoInclude();
            builder.Navigation(ae => ae.AccountNavigation).AutoInclude();

            builder.HasOne(a => a.AccountNavigation)
                .WithMany(ae => ae.AccountEventCollection)
                .HasForeignKey(ae => ae.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
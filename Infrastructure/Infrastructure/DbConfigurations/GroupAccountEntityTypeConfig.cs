using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class GroupAccountEntityTypeConfig : IEntityTypeConfiguration<GroupAccount>
    {
        public void Configure(EntityTypeBuilder<GroupAccount> builder)
        {
            builder.ToTable(nameof(GroupAccount));

            builder.HasKey(g => new { g.IdGroup, g.IdAccount });

            builder.Navigation(g => g.GroupNavigation).AutoInclude();
            builder.Navigation(g => g.AccountNavigation).AutoInclude();
        }
    }
}
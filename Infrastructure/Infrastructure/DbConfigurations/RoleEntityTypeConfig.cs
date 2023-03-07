using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal class RoleEntityTypeConfig : IEntityTypeConfiguration<IdentityRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
            builder.HasData(
            new IdentityRole<int>
            {
                Id = 1,
                Name = "Company",
                NormalizedName = "COMPANY"
            },
            new IdentityRole<int>
            {
                Id = 2,
                Name = "PrivateUser",
                NormalizedName = "PRIVATE_USER"
            }
        );
        }
    }
}
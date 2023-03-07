using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class GroupNameEntityTypeConfig : IEntityTypeConfiguration<GroupName>
    {
        public void Configure(EntityTypeBuilder<GroupName> builder)
        {
            builder.ToTable(nameof(GroupName));

            builder.HasKey(g => g.IdGroupName);
            builder.Property(g => g.IdGroupName).IsRequired().ValueGeneratedOnAdd();

            builder.Property(g => g.Name).IsRequired().HasMaxLength(200);
            builder.HasIndex(g => g.Name).IsUnique();

            builder.HasData(
                new GroupName
                {
                    IdGroupName = 1,
                    Name = "The Donation City"
                },
                new GroupName
                {
                    IdGroupName = 2,
                    Name = "Givers Of Tomorrow Organization"
                },
                new GroupName
                {
                    IdGroupName = 3,
                    Name = "Citizens Of Change"
                },
                new GroupName
                {
                    IdGroupName = 4,
                    Name = "The Care Club"
                },
                new GroupName
                {
                    IdGroupName = 5,
                    Name = "The Life Changers"
                },
                new GroupName
                {
                    IdGroupName = 6,
                    Name = "The Charity First"
                },
                new GroupName
                {
                    IdGroupName = 7,
                    Name = "Support For Tomorrow"
                },
                new GroupName
                {
                    IdGroupName = 8,
                    Name = "Spreading Smiles"
                },
                new GroupName
                {
                    IdGroupName = 9,
                    Name = "Urban Motive Charity"
                },
                new GroupName
                {
                    IdGroupName = 10,
                    Name = "Awesome Treasures Foundation"
                },
                new GroupName
                {
                    IdGroupName = 11,
                    Name = "The Generous Hearts"
                },
                new GroupName
                {
                    IdGroupName = 12,
                    Name = "The Right Cause"
                },
                new GroupName
                {
                    IdGroupName = 13,
                    Name = "Love In Action"
                },
                new GroupName
                {
                    IdGroupName = 14,
                    Name = "The Hands On Network"
                },
                new GroupName
                {
                    IdGroupName = 15,
                    Name = "Share the Love"
                },
                new GroupName
                {
                    IdGroupName = 16,
                    Name = "The Sharers Of Joy"
                },
                new GroupName
                {
                    IdGroupName = 17,
                    Name = "Helping Hands, Helping Smiles"
                },
                new GroupName
                {
                    IdGroupName = 18,
                    Name = "Worthy Of Hope"
                },
                new GroupName
                {
                    IdGroupName = 19,
                    Name = "The Charity Service"
                },
                new GroupName
                {
                    IdGroupName = 20,
                    Name = "Lifeline Express"
                },
                new GroupName
                {
                    IdGroupName = 21,
                    Name = "Active Love"
                },
                new GroupName
                {
                    IdGroupName = 22,
                    Name = "Worthy Purpose"
                },
                new GroupName
                {
                    IdGroupName = 23,
                    Name = "Grateful Hearts"
                },
                new GroupName
                {
                    IdGroupName = 24,
                    Name = "Love Life Charity"
                },
                new GroupName
                {
                    IdGroupName = 25,
                    Name = "Nation Donations"
                }
                );
        }
    }
}
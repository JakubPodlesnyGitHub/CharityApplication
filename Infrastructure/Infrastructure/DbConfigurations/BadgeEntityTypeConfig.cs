using Application.Providers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class BadgeEntityTypeConfig : IEntityTypeConfiguration<Badge>
    {
        public void Configure(EntityTypeBuilder<Badge> builder)
        {
            builder.ToTable(nameof(Badge));

            builder.HasKey(b => b.IdBadge);
            builder.Property(b => b.IdBadge).IsRequired().ValueGeneratedOnAdd();

            builder.Property(b => b.Name).IsRequired().HasMaxLength(200);
            builder.Property(b => b.BadgeGoal).IsRequired();
            builder.Property(b => b.Pointstreshold_Group).IsRequired();
            builder.Property(b => b.Pointstreshold_User).IsRequired();

            builder.HasData(
                new Badge
                {
                    IdBadge = 1,
                    Name = "Badge Level 1",
                    BadgeGoal = "Was acquired 50 points by a user or 55 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level1.png"),
                    Pointstreshold_User = 50,
                    Pointstreshold_Group = 55
                },
                new Badge
                {
                    IdBadge = 2,
                    Name = "Badge Level 2",
                    BadgeGoal = "Was acquired 100 points by a user or 110 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level2.png"),
                    Pointstreshold_User = 100,
                    Pointstreshold_Group = 110
                },
                new Badge
                {
                    IdBadge = 3,
                    Name = "Badge Level 3",
                    BadgeGoal = "Was acquired 150 points by a user or 165 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level3.png"),
                    Pointstreshold_User = 150,
                    Pointstreshold_Group = 165
                },
                new Badge
                {
                    IdBadge = 4,
                    Name = "Badge Level 4",
                    BadgeGoal = "Was acquired 200 points by a user or 220 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level4.png"),
                    Pointstreshold_User = 200,
                    Pointstreshold_Group = 220
                },
                new Badge
                {
                    IdBadge = 5,
                    Name = "Badge Level 5",
                    BadgeGoal = "Was acquired 250 points by a user or 275 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level5.png"),
                    Pointstreshold_User = 250,
                    Pointstreshold_Group = 275
                },
                new Badge
                {
                    IdBadge = 6,
                    Name = "Badge Level 6",
                    BadgeGoal = "Was acquired 300 points by a user or 330 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level6.png"),
                    Pointstreshold_User = 300,
                    Pointstreshold_Group = 330
                },
                new Badge
                {
                    IdBadge = 7,
                    Name = "Badge Level 7",
                    BadgeGoal = "Was acquired 350 points by a user or 380 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level7.png"),
                    Pointstreshold_User = 350,
                    Pointstreshold_Group = 380
                },
                new Badge
                {
                    IdBadge = 8,
                    Name = "Badge Level 8",
                    BadgeGoal = "Was acquired 400 points by a user or 440 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level8.png"),
                    Pointstreshold_User = 400,
                    Pointstreshold_Group = 440
                },
                new Badge
                {
                    IdBadge = 9,
                    Name = "Badge Level 9",
                    BadgeGoal = "Was acquired 450 points by a user or 495 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level9.png"),
                    Pointstreshold_User = 450,
                    Pointstreshold_Group = 495
                },
                new Badge
                {
                    IdBadge = 10,
                    Name = "Badge Level 10",
                    BadgeGoal = "Was acquired 500 points by a user or 550 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level10.png"),
                    Pointstreshold_User = 500,
                    Pointstreshold_Group = 550
                },
                new Badge
                {
                    IdBadge = 11,
                    Name = "Badge Level 11",
                    BadgeGoal = "Was acquired 550 points by a user or 605 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level11.png"),
                    Pointstreshold_User = 550,
                    Pointstreshold_Group = 605
                },
                new Badge
                {
                    IdBadge = 12,
                    Name = "Badge Level 12",
                    BadgeGoal = "Was acquired 600 points by a user or 660 points by a group",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Badges\level12.png"),
                    Pointstreshold_User = 600,
                    Pointstreshold_Group = 660
                }
                );
        }
    }
}
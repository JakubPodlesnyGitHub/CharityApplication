using Application.Providers;
using CharityApplication.Shared.Model.JsonWrappers.Module.BasicModules;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Infrastructure.DbConfigurations
{
    internal sealed class ModuleEntityTypeConfig : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable(nameof(Module));

            builder.HasKey(m => m.IdModule);
            builder.Property(m => m.IdModule).IsRequired().ValueGeneratedOnAdd();

            builder.Property(m => m.ModuleName).IsRequired().HasMaxLength(200);
            builder.HasIndex(m => m.ModuleName).IsUnique();
            builder.Property(m => m.ModuleDesc).IsRequired();
            builder.Property(m => m.ModuleJson).IsRequired();

            builder.HasMany(e => e.EventModules)
               .WithOne(em => em.ModuleNavigation)
               .HasForeignKey(em => em.IdModule)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Module
                {
                    IdModule = 1,
                    ModuleName = "Location",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Modules\location.png"),
                    ModuleDesc = "By selecting this module, you can display a given location on the map or display a route between two locations",
                    ModuleJson = JsonSerializer.Serialize(new LocationDataWrapper())
                },
                new Module
                {
                    IdModule = 2,
                    ModuleName = "Online Event",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Modules\online.png"),
                    ModuleDesc = "By selecting this module, it is possible to mark the event as online and last for a certain amount of time",
                    ModuleJson = JsonSerializer.Serialize(new MoudlePresenceWrapper())
                },
                new Module
                {
                    IdModule = 3,
                    ModuleName = "Foundraiser",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Modules\fundraiser.png"),
                    ModuleDesc = "By choosing this module, the user creates a virtual fundraiser to collect funds for a worthy cause",
                    ModuleJson = JsonSerializer.Serialize(new FoundraizerWrapper())
                },
                new Module
                {
                    IdModule = 4,
                    ModuleName = "QrCode",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Modules\qrcode.png"),
                    ModuleDesc = "By choosing this module you get a qr code thanks to which event members will be able to confirm their presence",
                    ModuleJson = JsonSerializer.Serialize(new MoudlePresenceWrapper())
                },
                new Module
                {
                    IdModule = 5,
                    ModuleName = "Assesment Form",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Modules\assessmentform.png"),
                    ModuleDesc = "By selecting this module you will get an event evaluation form that is displayed for each member of the event",
                    ModuleJson = JsonSerializer.Serialize(new MoudlePresenceWrapper())
                },
                new Module
                {
                    IdModule = 6,
                    ModuleName = "Attendance List",
                    Base64dataPicture = ImageBase64StringProvider.ProvideBase64(@"Images\Modules\attendance.png"),
                    ModuleDesc = "By selecting this module you will get a list to check the presence of event participants",
                    ModuleJson = JsonSerializer.Serialize(new MoudlePresenceWrapper())
                });
        }
    }
}
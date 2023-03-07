using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    internal sealed class ContactFormEntityTypeConfig : IEntityTypeConfiguration<ContactForm>
    {
        public void Configure(EntityTypeBuilder<ContactForm> builder)
        {
            builder.ToTable(nameof(ContactForm));

            builder.HasKey(c => c.IdContactForm);
            builder.Property(c => c.IdContactForm).IsRequired().ValueGeneratedOnAdd();

            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Subject).IsRequired();
            builder.Property(c => c.Message).IsRequired();
            builder.Property(c => c.Mail).IsRequired().HasMaxLength(100);
        }
    }
}
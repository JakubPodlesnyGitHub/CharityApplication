using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class CharityApplicationDbContext : IdentityDbContext<Account, IdentityRole<int>, int>
    {
        public CharityApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<PrivateAccount> PrivateAccounts { get; set; }
        public virtual DbSet<CompanyAccount> CompanyAccounts { get; set; }
        public virtual DbSet<CompanyAddress> CompanyAddresses { get; set; }
        public virtual DbSet<CompanyRepresentative> CompanyRepresentatives { get; set; }
        public virtual DbSet<AccountEvent> AccountEvents { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<BadgeAccount> BadgeAccounts { get; set; }
        public virtual DbSet<BadgeGroup> BadgeGroups { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupName> GroupNames { get; set; }
        public virtual DbSet<GroupAccount> GroupAccounts { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<AssesmentForm> AssesmentForms { get; set; }
        public virtual DbSet<ContactForm> ContactForms { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<EventModule> EventModules { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<GroupEvent> GroupEvents { get; set; }
        public virtual DbSet<GroupAnnouncement> GroupAnnouncements { get; set; }
        public virtual DbSet<EventAnnouncement> EventAnnouncements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(CharityApplicationDbContext).Assembly);
        }
    }
}
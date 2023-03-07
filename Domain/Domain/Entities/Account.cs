using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public partial class Account : IdentityUser<int>
    {
        public Account()
        {
            AccountGroupCollection = new HashSet<GroupAccount>();
            AccountEventCollection = new HashSet<AccountEvent>();
            AccountBadgeCollection = new HashSet<BadgeAccount>();
        }

        public bool VerificationStatus { get; set; }
        public bool GoldSponsorBadge { get; set; }
        public int Points { get; set; }
        public string? Base64dataPicture { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual CompanyAccount CompanyAccountNavigation { get; set; }
        public virtual PrivateAccount PrivateAccountNavigation { get; set; }
        public virtual ICollection<GroupAccount> AccountGroupCollection { get; set; }
        public virtual ICollection<AccountEvent> AccountEventCollection { get; set; }
        public virtual ICollection<BadgeAccount> AccountBadgeCollection { get; set; }
    }
}
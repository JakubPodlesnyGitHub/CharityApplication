using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class GroupAnnouncement
    {
        public int IdGroupAnnouncement { get; set; }
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int IdGroup { get; set; }
        public int IdOwner { get; set; }

        [ForeignKey(nameof(IdGroup))]
        public virtual Group GroupNavigation { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class EventAnnouncement
    {
        public int IdEventAnnouncement { get; set; }
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int IdEvent { get; set; }
        public int IdOwner { get; set; }

        [ForeignKey(nameof(IdEvent))]
        public virtual Event EventNavigation { get; set; }
    }
}
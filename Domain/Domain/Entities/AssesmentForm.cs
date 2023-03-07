using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class AssesmentForm
    {
        public int Id { get; set; }
        public string Mail { get; set; } = null!;
        public int EventRate { get; set; }
        public string Subject { get; set; } = null!;
        public int AppRate { get; set; }
        public string Message { get; set; } = null!;
        public int IdEvent { get; set; }
        public int IdOwner { get; set; }

        [ForeignKey(nameof(IdEvent))]
        public virtual Event EventNavigation { get; set; }
    }
}
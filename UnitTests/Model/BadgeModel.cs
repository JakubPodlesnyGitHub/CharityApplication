using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Model
{
    public class BadgeModel
    {
        public int IdBadge { get; set; }
        public string Name { get; set; } = null!;
        public string BadgeGoal { get; set; } = null!;
        public string? Base64dataPicture { get; set; }
        public int Pointstreshold_User { get; set; }
        public int Pointstreshold_Group { get; set; }
    }
}

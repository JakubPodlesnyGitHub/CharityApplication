using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Model
{
    public class ModuleModel
    {
        public int IdModule { get; set; }
        public string ModuleName { get; set; } = null!;
        public string ModuleDesc { get; set; } = null!;
        public string ModuleJson { get; set; } = null!;
        public string? Base64dataPicture { get; set; }
    }
}

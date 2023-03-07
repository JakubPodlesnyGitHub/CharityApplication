using CharityApplication.Client.Model.Error;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Application.Dtos.BasicDtos.Responses
{
    public sealed class BasicModuleDTO : ErrorResponseDTO
    {
        public int IdModule { get; set; }
        public string ModuleName { get; set; } = null!;
        public string ModuleDesc { get; set; } = null!;
        public string ModuleJson { get; set; } = null!;
        public string? Base64dataPicture { get; set; }

        [JsonIgnore]
        public HashSet<string> PropertiesToExclude { get; set; } = new HashSet<string> { "OriginAddress", "DestinationAddress" };

        [JsonIgnore]
        public bool ShowModuleProperties { get; set; } = false;

        [JsonIgnore]
        public object ModuleWrapper { get; set; }

        [JsonIgnore]
        public PropertyInfo[] Properties { get; set; }
    }
}
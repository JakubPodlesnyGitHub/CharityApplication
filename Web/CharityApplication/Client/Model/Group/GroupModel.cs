using Application.Dtos.BasicDtos.Responses;
using System.Text.Json.Serialization;

namespace CharityApplication.Client.Model.Group
{
    public class GroupModel
    {
        [JsonIgnore]
        public BasicGroupNameDTO GroupName { get; set; } = null!;

        public int? IdGroup { get; set; }
        public int NumberOfParticipants { get; set; }
        public string Description { get; set; } = null!;
        public bool GroupType { get; set; }
        public int IdGroupName { get => GroupName.IdGroupName; }
        public string? Base64dataPicture { get; set; }
        public int IdGroupOwner { get; set; }
    }
}
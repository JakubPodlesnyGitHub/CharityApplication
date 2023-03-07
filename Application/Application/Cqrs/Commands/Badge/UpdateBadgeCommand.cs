using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Badge
{
    public class UpdateBadgeCommand : IRequest<BasicBadgeDTO>
    {
        public int IdBadge { get; set; }
        public string Name { get; set; } = null!;
        public string BadgeGoal { get; set; } = null!;
        public int Pointstreshold_User { get; set; }
        public int Pointstreshold_Group { get; set; }

        public UpdateBadgeCommand(int idBadge, string name, string badgeGoal, int pointstreshold_User, int pointstreshold_Group)
        {
            IdBadge = idBadge;
            Name = name;
            BadgeGoal = badgeGoal;
            Pointstreshold_User = pointstreshold_User;
            Pointstreshold_Group = pointstreshold_Group;
        }
    }
}
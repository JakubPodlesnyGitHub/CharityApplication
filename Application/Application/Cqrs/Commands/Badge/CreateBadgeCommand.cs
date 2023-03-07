using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Badge
{
    public class CreateBadgeCommand : IRequest<BasicBadgeDTO>
    {
        public string Name { get; set; } = null!;
        public string BadgeGoal { get; set; } = null!;
        public int Pointstreshold_User { get; set; }
        public int Pointstreshold_Group { get; set; }

        public CreateBadgeCommand(string name, string badgeGoal, int pointstreshold_User, int pointstreshold_Group)
        {
            Name = name;
            BadgeGoal = badgeGoal;
            Pointstreshold_User = pointstreshold_User;
            Pointstreshold_Group = pointstreshold_Group;
        }
    }
}
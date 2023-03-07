using Application.Cqrs.Commands.BadgeGroup;
using Application.Cqrs.Queries.BadgeGroup;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class BadgeGroupHandler :
        IRequestHandler<GetBadgesGroupsQuery, List<BasicBadgeGroupDTO>>,
        IRequestHandler<GetGroupBadgesByGroupIdQury, List<BasicBadgeGroupDTO>>,
        IRequestHandler<GetBadgeGroupByIdQuery, BasicBadgeGroupDTO>,
        IRequestHandler<CreateBadgeGroupCommand, BasicBadgeGroupDTO>,
        IRequestHandler<DeleteBadgeGroupCommand, BasicBadgeGroupDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBadgeGroupRepository _badgeGroupRepository;
        private readonly IMapper _mapper;

        public BadgeGroupHandler(IUnitOfWork unitOfWork, IBadgeGroupRepository badgeGroupRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _badgeGroupRepository = badgeGroupRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicBadgeGroupDTO>> Handle(GetBadgesGroupsQuery request, CancellationToken cancellationToken)
        {
            var badgesGroups = await _badgeGroupRepository.GetAll();
            var badgesGroupsResponse = _mapper.Map<List<BasicBadgeGroupDTO>>(badgesGroups);
            return badgesGroupsResponse;
        }

        public async Task<BasicBadgeGroupDTO> Handle(GetBadgeGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var badgeGroup = await _badgeGroupRepository.GetById(request.IdGroup, request.IdBadge);
            if (badgeGroup is not null)
            {
                throw new NotFoundRecordException($"There is no connection between badge and group with given badge Id: {request.IdBadge} and group Id {request.IdGroup}.");
            }
            return _mapper.Map<BasicBadgeGroupDTO>(badgeGroup);
        }

        public async Task<BasicBadgeGroupDTO> Handle(CreateBadgeGroupCommand request, CancellationToken cancellationToken)
        {
            var newBadgeGroup = await _badgeGroupRepository.Insert(_mapper.Map<BadgeGroup>(request));
            await _unitOfWork.Commit();
            var newBadgeGroupResponse = _mapper.Map<BasicBadgeGroupDTO>(newBadgeGroup);
            return newBadgeGroupResponse;
        }

        public async Task<BasicBadgeGroupDTO> Handle(DeleteBadgeGroupCommand request, CancellationToken cancellationToken)
        {
            var deletedBadgeGroup = await _badgeGroupRepository.Delete(request.IdBadge, request.IdGroup);
            if (deletedBadgeGroup is not null)
            {
                throw new NotFoundRecordException($"There is no connection between badge and group with given badge Id: {request.IdBadge} and group Id {request.IdGroup}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicBadgeGroupDTO>(deletedBadgeGroup);
        }

        public async Task<List<BasicBadgeGroupDTO>> Handle(GetGroupBadgesByGroupIdQury request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicBadgeGroupDTO>>(await _badgeGroupRepository.GetGroupBadgesByGroupId(request.IdGroup));
        }
    }
}
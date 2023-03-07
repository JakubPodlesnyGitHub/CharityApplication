using Application.Cqrs.Commands.Group;
using Application.Cqrs.Queries.Group;
using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class GroupHandler :
        IRequestHandler<GetGroupsQuery, List<BasicGroupDTO>>,
        IRequestHandler<GetPublicPrivateGroupsQuery, List<BasicGroupDTO>>,
        IRequestHandler<GetVisibleGroups, List<BasicGroupDTO>>,
        IRequestHandler<GetGroupsByAccountIdQuery, List<BasicGroupDTO>>,
        IRequestHandler<GetGroupsByEventIdQuery, List<BasicGroupDTO>>,
        IRequestHandler<GetTopGroupsWithMostBadgePointsQuery, List<GroupWithBadgePointsDTO>>,
        IRequestHandler<GetGroupByIdQuery, BasicGroupDTO>,
        IRequestHandler<CreateGroupCommand, BasicGroupDTO>,
        IRequestHandler<DeleteGroupCommand, BasicGroupDTO>,
        IRequestHandler<UpdateGroupCommand, BasicGroupDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicGroupDTO>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupDTO>>(await _groupRepository.GetAll());
        }

        public async Task<BasicGroupDTO> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetById(request.IdGroup);
            if (group is null)
            {
                throw new NotFoundRecordException($"There is no group with given Id: {request.IdGroup}.");
            }
            return _mapper.Map<BasicGroupDTO>(group);
        }

        public async Task<BasicGroupDTO> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var newGroup = await _groupRepository.Insert(_mapper.Map<Group>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupDTO>(newGroup);
        }

        public async Task<BasicGroupDTO> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var deletedGroup = await _groupRepository.Delete(_mapper.Map<Group>(request));
            if (deletedGroup is null)
            {
                throw new NotFoundRecordException($"There is no group with given Id: {request.IdGroup}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupDTO>(deletedGroup);
        }

        public async Task<BasicGroupDTO> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var groupToUpdate = await _groupRepository.GetById(request.IdGroup);
            if (groupToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no group with given Id: {request.IdGroup}.");
            }
            if (request.Description is not null)
            {
                groupToUpdate.Description = request.Description;
            }

            groupToUpdate.IdGroupName = request.IdGroupName;
            groupToUpdate.NumberOfParticipants = request.NumberOfParticipants;
            groupToUpdate.GroupType = request.GroupType;

            await _groupRepository.Update(groupToUpdate);
            await _unitOfWork.Commit();

            return _mapper.Map<BasicGroupDTO>(groupToUpdate);
        }

        public async Task<List<GroupWithBadgePointsDTO>> Handle(GetTopGroupsWithMostBadgePointsQuery request, CancellationToken cancellationToken)
        {
            return await _groupRepository.GetTopgGroupsWithBadgePoints(request.NumberOfGroups);
        }

        public async Task<List<BasicGroupDTO>> Handle(GetGroupsByAccountIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupDTO>>(await _groupRepository.GetGroupsByAccountId(request.IdAccount));
        }

        public async Task<List<BasicGroupDTO>> Handle(GetGroupsByEventIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupDTO>>(await _groupRepository.GetGroupsByEventId(request.IdEvent));
        }

        public async Task<List<BasicGroupDTO>> Handle(GetPublicPrivateGroupsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupDTO>>(await _groupRepository.GetPublicPrivateGroups(request.IdAccount));
        }

        public async Task<List<BasicGroupDTO>> Handle(GetVisibleGroups request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetPublicGroups();
            var groupsReposne = _mapper.Map<List<BasicGroupDTO>>(groups);
            return groupsReposne;
        }
    }
}
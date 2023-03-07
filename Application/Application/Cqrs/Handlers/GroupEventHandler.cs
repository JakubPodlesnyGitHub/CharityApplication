using Application.Cqrs.Commands.GroupEvent;
using Application.Cqrs.Queries.GroupEvent;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class GroupEventHandler :
        IRequestHandler<GetGroupsEventsQuery, List<BasicGroupEventDTO>>,
        IRequestHandler<GetEventGroupsByEventIdQuery, List<BasicGroupEventDTO>>,
        IRequestHandler<GetGroupEventByIdQuery, BasicGroupEventDTO>,
        IRequestHandler<CreateGroupEventCommand, BasicGroupEventDTO>,
        IRequestHandler<DeleteGroupEventCommand, BasicGroupEventDTO>,
        IRequestHandler<UpdateGroupEventCommand, BasicGroupEventDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupEventRepository _groupEventRepository;
        private readonly IBadgeGroupRepository _badgeGroupRepository;
        private readonly IBadgeRepository _badgeRepository;
        private readonly IEventAccountRepository _eventAccountRepository;
        private readonly IGroupAccountRepository _groupAccountRepository;
        private readonly int POINTS_FOR_EVENT = 30;

        public GroupEventHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IGroupEventRepository groupEventRepository,
            IEventAccountRepository eventAccountRepository,
            IGroupAccountRepository groupAccountRepository,
            IBadgeGroupRepository badgeGroupRepository,
            IBadgeRepository badgeRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _groupEventRepository = groupEventRepository;
            _eventAccountRepository = eventAccountRepository;
            _groupAccountRepository = groupAccountRepository;
            _badgeGroupRepository = badgeGroupRepository;
            _badgeRepository = badgeRepository;
        }

        public async Task<List<BasicGroupEventDTO>> Handle(GetGroupsEventsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupEventDTO>>(await _groupEventRepository.GetAll());
        }

        public async Task<List<BasicGroupEventDTO>> Handle(GetEventGroupsByEventIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupEventDTO>>(await _groupEventRepository.GetEventGroupsByEventId(request.IdEvent));
        }

        public async Task<BasicGroupEventDTO> Handle(GetGroupEventByIdQuery request, CancellationToken cancellationToken)
        {
            var groupEvent = await _groupEventRepository.GetById(request.IdGroup, request.IdEvent);
            if (groupEvent is null)
            {
                throw new NotFoundRecordException($"There is no connection between group and event with given group Id: {request.IdGroup} and event Id {request.IdEvent}.");
            }
            return _mapper.Map<BasicGroupEventDTO>(groupEvent);
        }

        public async Task<BasicGroupEventDTO> Handle(CreateGroupEventCommand request, CancellationToken cancellationToken)
        {
            await CheckNumberOfGroupAccountsParticipantsOfTheEventMembers(request);
            var groupEvent = await _groupEventRepository.Insert(_mapper.Map<GroupEvent>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupEventDTO>(groupEvent);
        }

        public async Task<BasicGroupEventDTO> Handle(DeleteGroupEventCommand request, CancellationToken cancellationToken)
        {
            var groupEventToDelete = await _groupEventRepository.Delete(_mapper.Map<GroupEvent>(request));
            if (groupEventToDelete is null)
            {
                throw new NotFoundRecordException($"There is no connection between group and event with given group Id: {request.IdGroup} and event Id {request.IdEvent}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupEventDTO>(groupEventToDelete);
        }

        public async Task<BasicGroupEventDTO> Handle(UpdateGroupEventCommand request, CancellationToken cancellationToken)
        {
            var groupEventToUpdate = await _groupEventRepository.GetById(request.IdGroup, request.IdEvent);

            if (groupEventToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no connection between group and event with given group Id: {request.IdGroup} and event Id {request.IdEvent}.");
            }

            groupEventToUpdate.IfParticipantEvent = request.IfParticipantEvent;

            groupEventToUpdate.GroupNavigation.Points += await GetPoints(groupEventToUpdate);

            await _groupEventRepository.Update(groupEventToUpdate);
            await _unitOfWork.Commit();
            var groupEventDTO = _mapper.Map<BasicGroupEventDTO>(groupEventToUpdate);
            await AddBadgesToGroup(groupEventDTO.Group);
            return groupEventDTO;
        }

        private async Task<int> GetPoints(GroupEvent groupEvent)
        {
            var groupAccounts = await _groupAccountRepository.GetGroupAccountsByGroupId(groupEvent.IdGroup);
            var eventPartcipants = await _groupAccountRepository.GetPartcipantOfEventGroupMembers(groupEvent.IdGroup, groupEvent.IdEvent);
            if (eventPartcipants.Count / groupAccounts.Count < 0.8)
            {
                throw new InsufficientNumberEventParticipantsException("The number of group participants who declared their presence at the event does not exceed 80 percent. " +
                   "The group cannot be joined to the event");
            }
            var extraUsers = eventPartcipants.Count - (int)Math.Ceiling(0.8 * groupAccounts.Count);
            int points = groupEvent.GroupNavigation.Points;
            if (groupEvent.IfParticipantEvent)
            {
                points += POINTS_FOR_EVENT + (int)Math.Ceiling(extraUsers * POINTS_FOR_EVENT * 0.10);
            }
            else
            {
                points = (groupEvent.GroupNavigation.Points - POINTS_FOR_EVENT) <= 0 ? 0 : groupEvent.GroupNavigation.Points -= POINTS_FOR_EVENT;
            }
            return points;
        }

        private async Task AddBadgesToGroup(BasicGroupDTO groupDTO)
        {
            var badges = await _badgeRepository.GetBadgesByGroupId(groupDTO.IdGroup);
            foreach (var badge in badges)
            {
                if (groupDTO.Points >= badge.Pointstreshold_Group)
                {
                    await _badgeGroupRepository.Insert(new BadgeGroup { IdGroup = groupDTO.IdGroup, IdBadge = badge.IdBadge });
                }
            }
            await _unitOfWork.Commit();
        }

        private async Task CheckNumberOfGroupAccountsParticipantsOfTheEventMembers(CreateGroupEventCommand request)
        {
            var groupAccounts = await _groupAccountRepository.GetGroupAccountsByGroupId(request.IdGroup);
            var eventPartcipants = await _groupAccountRepository.GetPartcipantOfEventGroupMembers(request.IdGroup, request.IdEvent);
            if (eventPartcipants.Count / groupAccounts.Count < 0.8)
            {
                throw new InsufficientNumberEventParticipantsException("The number of group participants who declared their presence at the event does not exceed 80 percent. " +
                    "The group cannot be joined to the event");
            }
        }
    }
}
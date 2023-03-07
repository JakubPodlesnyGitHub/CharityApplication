using Application.Cqrs.Commands.GroupAnnouncement;
using Application.Cqrs.Queries.GroupAnnouncement;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class GroupAnnouncementHandler :
        IRequestHandler<GetGroupsAnnouncementsQuery, List<BasicGroupAnnouncementDTO>>,
        IRequestHandler<GetGroupAnnouncementsByGroupIdQuery, List<BasicGroupAnnouncementDTO>>,
        IRequestHandler<GetGroupAnnouncementByIdQuery, BasicGroupAnnouncementDTO>,
        IRequestHandler<CreateGroupAnnouncementCommand, BasicGroupAnnouncementDTO>,
        IRequestHandler<DeleteGroupAnnouncementCommand, BasicGroupAnnouncementDTO>,
        IRequestHandler<UpdateGroupAnnouncementCommand, BasicGroupAnnouncementDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupAnnouncementRepository _groupAnnouncementRepository;
        private readonly IMapper _mapper;

        public GroupAnnouncementHandler(IUnitOfWork unitOfWork, IGroupAnnouncementRepository groupAnnouncementRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _groupAnnouncementRepository = groupAnnouncementRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicGroupAnnouncementDTO>> Handle(GetGroupsAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupAnnouncementDTO>>(await _groupAnnouncementRepository.GetAll());
        }

        public async Task<List<BasicGroupAnnouncementDTO>> Handle(GetGroupAnnouncementsByGroupIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupAnnouncementDTO>>(await _groupAnnouncementRepository.GetGroupAnnouncementsByGroupId(request.IdGroup));
        }

        public async Task<BasicGroupAnnouncementDTO> Handle(CreateGroupAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var newGroupAnnouncement = await _groupAnnouncementRepository.Insert(_mapper.Map<GroupAnnouncement>(request));
            if (newGroupAnnouncement is null)
            {
                throw new NotFoundRecordException($"There is no group announcement with given Id: {request.IdGroup}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupAnnouncementDTO>(newGroupAnnouncement);
        }

        public async Task<BasicGroupAnnouncementDTO> Handle(DeleteGroupAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var deletedGroupAnnouncement = await _groupAnnouncementRepository.Delete(request.IdGroupAnnouncement);
            if (deletedGroupAnnouncement is null)
            {
                throw new NotFoundRecordException($"There is no group announcement with given Id: {request.IdGroupAnnouncement}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupAnnouncementDTO>(deletedGroupAnnouncement);
        }

        public async Task<BasicGroupAnnouncementDTO> Handle(UpdateGroupAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var groupAnnouncementToUpdate = await _groupAnnouncementRepository.GetById(request.IdGroupAnnouncement);

            if (groupAnnouncementToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no group announcement with given Id: {request.IdGroupAnnouncement}.");
            }

            if (request.Subject is not null)
            {
                groupAnnouncementToUpdate.Subject = request.Subject;
            }
            if (request.Message is not null)
            {
                groupAnnouncementToUpdate.Message = request.Message;
            }
            if (request.IdGroup != groupAnnouncementToUpdate.IdGroup)
            {
                groupAnnouncementToUpdate.IdGroup = request.IdGroup;
            }
            if (request.IdOwner != groupAnnouncementToUpdate.IdOwner)
            {
                groupAnnouncementToUpdate.IdOwner = request.IdOwner;
            }

            await _groupAnnouncementRepository.Update(groupAnnouncementToUpdate);
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupAnnouncementDTO>(groupAnnouncementToUpdate);
        }

        public async Task<BasicGroupAnnouncementDTO> Handle(GetGroupAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            var groupAnnouncement = await _groupAnnouncementRepository.GetById(request.IdGroupAnnouncement);
            if (groupAnnouncement is null)
            {
                throw new NotFoundRecordException($"There is no group announcement with given Id: {request.IdGroupAnnouncement}.");
            }
            return _mapper.Map<BasicGroupAnnouncementDTO>(groupAnnouncement);
        }
    }
}
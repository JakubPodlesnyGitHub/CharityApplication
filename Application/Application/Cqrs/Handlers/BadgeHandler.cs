using Application.Cqrs.Commands.Badge;
using Application.Cqrs.Queries.Badge;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class BadgeHandler :
        IRequestHandler<GetBadgesQuery, List<BasicBadgeDTO>>,
        IRequestHandler<GetBadgeQueryId, BasicBadgeDTO>,
        IRequestHandler<CreateBadgeCommand, BasicBadgeDTO>,
        IRequestHandler<DeleteBadgeCommand, BasicBadgeDTO>,
        IRequestHandler<UpdateBadgeCommand, BasicBadgeDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBadgeRepository _badgeRepository;
        private readonly IMapper _mapper;

        public BadgeHandler(IUnitOfWork unitOfWork, IBadgeRepository badgeRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _badgeRepository = badgeRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicBadgeDTO>> Handle(GetBadgesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicBadgeDTO>>(await _badgeRepository.GetAll());
        }

        public async Task<BasicBadgeDTO> Handle(DeleteBadgeCommand request, CancellationToken cancellationToken)
        {
            var deletedBadge = await _badgeRepository.Delete(request.IdBadge);
            if (deletedBadge is null)
            {
                throw new NotFoundRecordException($"There is no badge with given Id: {request.IdBadge}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicBadgeDTO>(deletedBadge);
        }

        public async Task<BasicBadgeDTO> Handle(CreateBadgeCommand request, CancellationToken cancellationToken)
        {
            var newBadge = await _badgeRepository.Insert(_mapper.Map<Badge>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicBadgeDTO>(newBadge);
        }

        public async Task<BasicBadgeDTO> Handle(GetBadgeQueryId request, CancellationToken cancellationToken)
        {
            var badge = await _badgeRepository.GetById(request.Id);
            if (badge is null)
            {
                throw new NotFoundRecordException($"There is no badge with given Id: {request.Id}.");
            }
            return _mapper.Map<BasicBadgeDTO>(badge);
        }

        public async Task<BasicBadgeDTO> Handle(UpdateBadgeCommand request, CancellationToken cancellationToken)
        {
            var badgeToUpdate = await _badgeRepository.GetById(request.IdBadge);
            if (badgeToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no badge with given Id: {request.IdBadge}");
            }

            if (badgeToUpdate.Name is not null)
            {
                badgeToUpdate.Name = request.Name;
            }
            if (badgeToUpdate.BadgeGoal is not null)
            {
                badgeToUpdate.BadgeGoal = request.BadgeGoal;
            }
            if (badgeToUpdate.Pointstreshold_User != request.Pointstreshold_User)
            {
                badgeToUpdate.Pointstreshold_User = request.Pointstreshold_User;
            }
            if (badgeToUpdate.Pointstreshold_Group != request.Pointstreshold_Group)
            {
                badgeToUpdate.Pointstreshold_Group = request.Pointstreshold_Group;
            }
            await _badgeRepository.Update(badgeToUpdate);
            await _unitOfWork.Commit();
            return _mapper.Map<BasicBadgeDTO>(badgeToUpdate);
        }
    }
}
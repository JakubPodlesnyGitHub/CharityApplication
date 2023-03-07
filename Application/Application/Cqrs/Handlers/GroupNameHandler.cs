using Application.Cqrs.Commands.GroupName;
using Application.Cqrs.Queries.GroupName;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class GroupNameHandler :
        IRequestHandler<GetGroupNamesQuery, List<BasicGroupNameDTO>>,
        IRequestHandler<GetGroupNameByIdQuery, BasicGroupNameDTO>,
        IRequestHandler<CreateGroupNameCommand, BasicGroupNameDTO>,
        IRequestHandler<DeleteGroupNameCommand, BasicGroupNameDTO>,
        IRequestHandler<UpdateGroupNameCommand, BasicGroupNameDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupNameRepository _groupNameRepository;
        private readonly IMapper _mapper;

        public GroupNameHandler(IUnitOfWork unitOfWork, IGroupNameRepository groupNameRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _groupNameRepository = groupNameRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicGroupNameDTO>> Handle(GetGroupNamesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupNameDTO>>(await _groupNameRepository.GetAll());
        }

        public async Task<BasicGroupNameDTO> Handle(GetGroupNameByIdQuery request, CancellationToken cancellationToken)
        {
            var groupName = await _groupNameRepository.GetById(request.Id);
            if (groupName is null)
            {
                throw new NotFoundRecordException($"There is no group Name with given Id: {request.Id}.");
            }
            return _mapper.Map<BasicGroupNameDTO>(groupName);
        }

        public async Task<BasicGroupNameDTO> Handle(DeleteGroupNameCommand request, CancellationToken cancellationToken)
        {
            var deletedGroupName = await _groupNameRepository.Delete(request.IdGroupName);
            if (deletedGroupName is null)
            {
                throw new NotFoundRecordException($"There is no group Name with given Id: {request.IdGroupName}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupNameDTO>(deletedGroupName);
        }

        public async Task<BasicGroupNameDTO> Handle(CreateGroupNameCommand request, CancellationToken cancellationToken)
        {
            var newGroupName = await _groupNameRepository.Insert(_mapper.Map<GroupName>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupNameDTO>(newGroupName);
        }

        public async Task<BasicGroupNameDTO> Handle(UpdateGroupNameCommand request, CancellationToken cancellationToken)
        {
            var groupNameToUpdate = await _groupNameRepository.GetById(request.IdGroupName);
            if (groupNameToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no group Name with given Id: {request.IdGroupName}.");
            }
            if (request.Name is not null)
            {
                groupNameToUpdate.Name = request.Name;
            }

            await _groupNameRepository.Update(groupNameToUpdate);
            await _unitOfWork.Commit();

            return _mapper.Map<BasicGroupNameDTO>(groupNameToUpdate);
        }
    }
}
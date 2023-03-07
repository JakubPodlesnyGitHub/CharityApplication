using Application.Cqrs.Commands.Status;
using Application.Cqrs.Queries.Status;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class StatusHandler :
        IRequestHandler<GetStatusesQuery, List<BasicStatusDTO>>,
        IRequestHandler<GetStatusByIdQuery, BasicStatusDTO>,
        IRequestHandler<CreateStatusCommand, BasicStatusDTO>,
        IRequestHandler<DeleteStatusCommand, BasicStatusDTO>,
        IRequestHandler<UpdateStatusCommand, BasicStatusDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusHandler(IUnitOfWork unitOfWork, IStatusRepository statusRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicStatusDTO>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicStatusDTO>>(await _statusRepository.GetAll());
        }

        public async Task<BasicStatusDTO> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.GetById(request.Id);
            if (status is null)
            {
                throw new NotFoundRecordException($"There is no status with given Id: {request.Id}.");
            }
            return _mapper.Map<BasicStatusDTO>(status);
        }

        public async Task<BasicStatusDTO> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            var newStatus = await _statusRepository.Insert(_mapper.Map<Status>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicStatusDTO>(newStatus);
        }

        public async Task<BasicStatusDTO> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            var deletedStatus = await _statusRepository.Delete(request.IdStatus);
            if (deletedStatus is null)
            {
                throw new NotFoundRecordException($"There is no status with given Id: {request.IdStatus}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicStatusDTO>(deletedStatus);
        }

        public async Task<BasicStatusDTO> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            var statusToUpdate = await _statusRepository.GetById(request.IdStatus);
            if (statusToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no status with given Id: {request.IdStatus}.");
            }
            if (request.Name is not null)
            {
                statusToUpdate.Name = request.Name;
            }
            await _statusRepository.Update(statusToUpdate);
            await _unitOfWork.Commit();

            return _mapper.Map<BasicStatusDTO>(statusToUpdate);
        }
    }
}
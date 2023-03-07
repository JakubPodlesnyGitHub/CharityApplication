using Application.Cqrs.Commands.Module;
using Application.Cqrs.Queries.Module;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class ModuleHandler :
        IRequestHandler<GetModulesQuery, List<BasicModuleDTO>>,
        IRequestHandler<GetModuleByIdQuery, BasicModuleDTO>,
        IRequestHandler<CreateModuleCommand, BasicModuleDTO>,
        IRequestHandler<DeleteModuleCommand, BasicModuleDTO>,
        IRequestHandler<UpdateModuleCommand, BasicModuleDTO>
    {
        private readonly IMapper _mapper;
        private readonly IModuleRepository _moduleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModuleHandler(IMapper mapper, IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _moduleRepository = moduleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BasicModuleDTO>> Handle(GetModulesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicModuleDTO>>(await _moduleRepository.GetAll());
        }

        public async Task<BasicModuleDTO> Handle(GetModuleByIdQuery request, CancellationToken cancellationToken)
        {
            var module = await _moduleRepository.GetById(request.IdModule);
            if (module is null)
            {
                throw new NotFoundRecordException($"There is no module with given Id: {request.IdModule}.");
            }
            return _mapper.Map<BasicModuleDTO>(module);
        }

        public async Task<BasicModuleDTO> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            var newModule = await _moduleRepository.Insert(_mapper.Map<Module>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicModuleDTO>(newModule);
        }

        public async Task<BasicModuleDTO> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleToDelete = await _moduleRepository.Delete(request.IdModule);
            if (moduleToDelete is null)
            {
                throw new NotFoundRecordException($"There is no module with given Id: {request.IdModule}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicModuleDTO>(moduleToDelete);
        }

        public async Task<BasicModuleDTO> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleToUpdate = await _moduleRepository.GetById(request.IdModule);
            if (moduleToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no module with given Id: {request.IdModule}.");
            }
            if (request.ModuleName is not null)
            {
                moduleToUpdate.ModuleName = request.ModuleName;
            }
            if (request.ModuleDesc is not null)
            {
                moduleToUpdate.ModuleDesc = request.ModuleDesc;
            }

            await _moduleRepository.Update(moduleToUpdate);
            await _unitOfWork.Commit();

            return _mapper.Map<BasicModuleDTO>(moduleToUpdate);
        }
    }
}
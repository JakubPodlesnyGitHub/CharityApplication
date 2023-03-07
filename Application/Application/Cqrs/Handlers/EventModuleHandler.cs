using Application.Cqrs.Commands.EventModule;
using Application.Cqrs.Queries.EventModule;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class EventModuleHandler :
        IRequestHandler<GetEventsModulesQuery, List<BasicEventModuleDTO>>,
        IRequestHandler<GetEventModulesListByEventIdQuery, List<BasicModuleDTO>>,
        IRequestHandler<GetEventModuleByIdQuery, BasicEventModuleDTO>,
        IRequestHandler<CreateEventModuleCommand, BasicEventModuleDTO>,
        IRequestHandler<DeleteEventModuleCommand, BasicEventModuleDTO>,
        IRequestHandler<UpdateEventModuleCommand, BasicEventModuleDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventModuleRepository _eventModuleRepository;

        public EventModuleHandler(IMapper mapper, IUnitOfWork unitOfWork, IEventModuleRepository eventModuleRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventModuleRepository = eventModuleRepository;
        }

        public async Task<List<BasicEventModuleDTO>> Handle(GetEventsModulesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicEventModuleDTO>>(await _eventModuleRepository.GetAll());
        }

        public async Task<List<BasicModuleDTO>> Handle(GetEventModulesListByEventIdQuery request, CancellationToken cancellationToken)
        {
            return await _eventModuleRepository.GetEventModulesListByEventId(request.IdEvent);
        }

        public async Task<BasicEventModuleDTO> Handle(GetEventModuleByIdQuery request, CancellationToken cancellationToken)
        {
            var eventModule = await _eventModuleRepository.GetById(request.IdEventModule);
            if (eventModule is null)
            {
                throw new NotFoundRecordException($"There is no connection beetwen event and module with given Id: {request.IdEventModule}.");
            }
            return _mapper.Map<BasicEventModuleDTO>(eventModule);
        }

        public async Task<BasicEventModuleDTO> Handle(CreateEventModuleCommand request, CancellationToken cancellationToken)
        {
            var newEventModule = await _eventModuleRepository.Insert(_mapper.Map<EventModule>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventModuleDTO>(newEventModule);
        }

        public async Task<BasicEventModuleDTO> Handle(DeleteEventModuleCommand request, CancellationToken cancellationToken)
        {
            var eventModuleToDelete = await _eventModuleRepository.Delete(request.IdEventModule);
            if (eventModuleToDelete is null)
            {
                throw new NotFoundRecordException($"There is no connection beetwen event and module with given Id: {request.IdEventModule}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventModuleDTO>(eventModuleToDelete);
        }

        public async Task<BasicEventModuleDTO> Handle(UpdateEventModuleCommand request, CancellationToken cancellationToken)
        {
            var eventModuleToUpdate = await _eventModuleRepository.GetById(request.IdEventModule);
            if (eventModuleToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no connection beetwen event and module with given Id: {request.IdEventModule}.");
            }
            if (request.IdModule != eventModuleToUpdate.IdModule)
            {
                eventModuleToUpdate.IdModule = request.IdModule;
            }
            if (request.IdEvent != eventModuleToUpdate.IdEvent)
            {
                eventModuleToUpdate.IdEvent = request.IdEvent;
            }
            await _eventModuleRepository.Update(eventModuleToUpdate);
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventModuleDTO>(eventModuleToUpdate);
        }
    }
}
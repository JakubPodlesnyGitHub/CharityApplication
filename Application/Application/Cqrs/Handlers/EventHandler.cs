using Application.Cqrs.Commands.Event;
using Application.Cqrs.Queries.Event;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class EventHandler :
        IRequestHandler<GetEventsQuery, List<BasicEventDTO>>,
        IRequestHandler<GetEventsByAccountIdQuery, List<BasicEventDTO>>,
        IRequestHandler<GetEventsByGroupIdQuery, List<BasicEventDTO>>,
        IRequestHandler<GetEventByIdQuery, BasicEventDTO>,
        IRequestHandler<CreateEventCommand, BasicEventDTO>,
        IRequestHandler<DeleteEventCommand, BasicEventDTO>,
        IRequestHandler<UpdateEventCommand, BasicEventDTO>,
        IRequestHandler<UpdateEventStatusCommand, BasicEventDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventHandler(IUnitOfWork unitOfWork, IEventRepository eventRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<BasicEventDTO> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = await _eventRepository.Insert(_mapper.Map<Event>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventDTO>(newEvent);
        }

        public async Task<BasicEventDTO> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await _eventRepository.GetById(request.IdEvent);
            if (eventToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no event with given event Id: {request.IdEvent}.");
            }
            if (request.EventName is not null)
            {
                eventToUpdate.EventName = request.EventName;
            }
            if (request.EventGoal is not null)
            {
                eventToUpdate.EventGoal = request.EventGoal;
            }
            if (request.EventDesc is not null)
            {
                eventToUpdate.EventDesc = request.EventDesc;
            }
            if (request.JsonEvent is not null)
            {
                eventToUpdate.JsonEvent = request.JsonEvent;
            }
            eventToUpdate.EventStartDate = request.EventStartDate.Value;
            eventToUpdate.EventEndDate = request.EventEndDate.Value;
            eventToUpdate.EventMemeberLimit = request.EventMemeberLimit.Value;
            eventToUpdate.IdStatus = request.IdStatus.Value;

            await _eventRepository.Update(eventToUpdate);
            await _unitOfWork.Commit();

            return _mapper.Map<BasicEventDTO>(eventToUpdate);
        }

        public async Task<BasicEventDTO> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var deletedEvent = await _eventRepository.Delete(request.Id);
            if (deletedEvent is null)
            {
                throw new NotFoundRecordException($"There is no event with given event Id: {request.Id}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventDTO>(deletedEvent);
        }

        public async Task<List<BasicEventDTO>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicEventDTO>>(await _eventRepository.GetAll());
        }

        public async Task<BasicEventDTO> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var charityEvent = await _eventRepository.GetById(request.Id);
            if (charityEvent is null)
            {
                throw new NotFoundRecordException($"There is no event with given event Id: {request.Id}.");
            }
            return _mapper.Map<BasicEventDTO>(charityEvent);
        }

        public async Task<List<BasicEventDTO>> Handle(GetEventsByAccountIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicEventDTO>>(await _eventRepository.GetRelatedEventsByAccountIdAsync(request.IdAccount));
        }

        public async Task<List<BasicEventDTO>> Handle(GetEventsByGroupIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicEventDTO>>(await _eventRepository.GetRelatedEventsByGroupIdAsync(request.IdGroup));
        }

        public async Task<BasicEventDTO> Handle(UpdateEventStatusCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await _eventRepository.GetById(request.IdEvent);
            if (eventToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no event with given event Id: {request.IdEvent}.");
            }
            if (eventToUpdate.IdStatus != request.IdStatus)
            {
                eventToUpdate.IdStatus = request.IdStatus;
            }
            await _eventRepository.Update(eventToUpdate);
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventDTO>(eventToUpdate);
        }
    }
}
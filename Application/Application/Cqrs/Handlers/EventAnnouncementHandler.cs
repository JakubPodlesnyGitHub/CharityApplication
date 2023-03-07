using Application.Cqrs.Commands.EventAnnouncement;
using Application.Cqrs.Queries.EventAnnouncement;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class EventAnnouncementHandler :
        IRequestHandler<GetEventsAnnouncementsQuery, List<BasicEventAnnouncementDTO>>,
        IRequestHandler<GetEventAnnouncementsByEventIdQuery, List<BasicEventAnnouncementDTO>>,
        IRequestHandler<GetEventAnnouncementByIdQuery, BasicEventAnnouncementDTO>,
        IRequestHandler<CreateEventAnnouncementCommand, BasicEventAnnouncementDTO>,
        IRequestHandler<UpdateEventAnnouncementCommand, BasicEventAnnouncementDTO>,
        IRequestHandler<DeleteEventAnnouncementCommand, BasicEventAnnouncementDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventAnnouncementRepository _eventAnnouncementRepository;
        private readonly IMapper _mapper;

        public EventAnnouncementHandler(IUnitOfWork unitOfWork, IEventAnnouncementRepository eventAnnouncementRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _eventAnnouncementRepository = eventAnnouncementRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicEventAnnouncementDTO>> Handle(GetEventAnnouncementsByEventIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicEventAnnouncementDTO>>(await _eventAnnouncementRepository.GetEventAnnouncementsByEventId(request.IdEvent));
        }

        public async Task<BasicEventAnnouncementDTO> Handle(CreateEventAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var newEventAnnouncement = await _eventAnnouncementRepository.Insert(_mapper.Map<EventAnnouncement>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventAnnouncementDTO>(newEventAnnouncement);
        }

        public async Task<BasicEventAnnouncementDTO> Handle(DeleteEventAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var eventAnnouncementToDelete = await _eventAnnouncementRepository.Delete(request.IdEventAnnouncement);
            if (eventAnnouncementToDelete is null)
            {
                throw new NotFoundRecordException($"There is no event announcement with given Id: {request.IdEventAnnouncement}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventAnnouncementDTO>(eventAnnouncementToDelete);
        }

        public async Task<BasicEventAnnouncementDTO> Handle(UpdateEventAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var eventAnnouncementToUpdate = await _eventAnnouncementRepository.GetById(request.IdEventAnnouncement);
            if (eventAnnouncementToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no event announcement with given Id: {request.IdEventAnnouncement}.");
            }
            if (request.Subject is not null)
            {
                eventAnnouncementToUpdate.Subject = request.Subject;
            }
            if (request.IdOwner != eventAnnouncementToUpdate.IdOwner)
            {
                eventAnnouncementToUpdate.IdOwner = request.IdOwner;
            }
            if (request.Message is not null)
            {
                eventAnnouncementToUpdate.Message = request.Message;
            }
            if (request.IdEvent != eventAnnouncementToUpdate.IdEvent)
            {
                eventAnnouncementToUpdate.IdEvent = request.IdEvent;
            }
            await _eventAnnouncementRepository.Update(eventAnnouncementToUpdate);
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventAnnouncementDTO>(eventAnnouncementToUpdate);
        }

        public async Task<List<BasicEventAnnouncementDTO>> Handle(GetEventsAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicEventAnnouncementDTO>>(await _eventAnnouncementRepository.GetAll());
        }

        public async Task<BasicEventAnnouncementDTO> Handle(GetEventAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            var eventAnnouncement = await _eventAnnouncementRepository.GetById(request.IdEventAnnouncement);
            if (eventAnnouncement is null)
            {
                throw new NotFoundRecordException($"There is no event announcement with given Id: {request.IdEventAnnouncement}.");
            }
            return _mapper.Map<BasicEventAnnouncementDTO>(eventAnnouncement);
        }
    }
}
using Application.Cqrs.Commands.EventAccount;
using Application.Cqrs.Queries.EventAccount;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class EventAccountHandler :
        IRequestHandler<GetEventsAccountsQuery, List<BasicEventAccountDTO>>,
        IRequestHandler<GetEventAccountsByEventIdQuery, List<BasicEventAccountDTO>>,
        IRequestHandler<CreateEventAccountCommand, BasicEventAccountDTO>,
        IRequestHandler<UpdateEventAccountCommand, BasicEventAccountDTO>,
        IRequestHandler<UpdateEventAccountSubsidyCommand, BasicEventAccountDTO>,
        IRequestHandler<DeleteEventAccountCommand, BasicEventAccountDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventAccountRepository _eventAccountRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IBadgeAccountRepository _badgeAccountRepository;
        private readonly IBadgeRepository _badgeRepository;
        private readonly IMapper _mapper;
        private readonly int POINTS_FOR_EVENT = 30;

        public EventAccountHandler(IUnitOfWork unitOfWork, IEventAccountRepository eventAccountRepository, IEventRepository eventRepository, IBadgeAccountRepository badgeAccountRepository, IBadgeRepository badgeRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _eventAccountRepository = eventAccountRepository;
            _eventRepository = eventRepository;
            _badgeAccountRepository = badgeAccountRepository;
            _badgeRepository = badgeRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicEventAccountDTO>> Handle(GetEventsAccountsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicEventAccountDTO>>(await _eventAccountRepository.GetAll());
        }

        public async Task<List<BasicEventAccountDTO>> Handle(GetEventAccountsByEventIdQuery request, CancellationToken cancellationToken)
        {
            return await _eventAccountRepository.GetEventAccountsByEventId(request.IdEvent);
        }

        public async Task<BasicEventAccountDTO> Handle(CreateEventAccountCommand request, CancellationToken cancellationToken)
        {
            await CheckNumberOfEventsMembers(request);
            var newEventAccount = await _eventAccountRepository.InsertAccountEvent(request);
            await AddPointsToAccount(newEventAccount, request.IfPartcipantPresent.Value, true);
            await AddBadgesToAccount(newEventAccount.AccountNavigation);
            return _mapper.Map<BasicEventAccountDTO>(newEventAccount);
        }

        public async Task<BasicEventAccountDTO> Handle(UpdateEventAccountCommand request, CancellationToken cancellationToken)
        {
            var eventAccount = await _eventAccountRepository.GetById(request.IdAccount, request.IdEvent);

            if (eventAccount is null)
            {
                throw new NotFoundRecordException($"There is no connection between event and account with given account Id: {request.IdAccount} and event Id: {request.IdEvent}.");
            }

            eventAccount.IfPartcipantPresent = request.IfPartcipantPresent;

            await AddPointsToAccount(eventAccount, request.IfPartcipantPresent);
            await AddBadgesToAccount(eventAccount.AccountNavigation);

            var eventAccountDTO = _mapper.Map<BasicEventAccountDTO>(eventAccount);
            return eventAccountDTO;
        }

        public async Task<BasicEventAccountDTO> Handle(UpdateEventAccountSubsidyCommand request, CancellationToken cancellationToken)
        {
            var eventAccountToUpdate = await _eventAccountRepository.GetById(request.IdAccount, request.IdEvent);
            if (eventAccountToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no connection between event and account with given account Id: {request.IdAccount} and event Id: {request.IdEvent}.");
            }
            eventAccountToUpdate.SubsidyAmount += 1;
            await _eventAccountRepository.Update(eventAccountToUpdate);
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventAccountDTO>(eventAccountToUpdate);
        }

        public async Task<BasicEventAccountDTO> Handle(DeleteEventAccountCommand request, CancellationToken cancellationToken)
        {
            var deletedEventAccount = await _eventAccountRepository.Delete(request.IdAccount, request.IdEvent);
            if (deletedEventAccount is null)
            {
                throw new NotFoundRecordException($"There is no connection between event and account with given account Id: {request.IdAccount} and event Id: {request.IdEvent}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicEventAccountDTO>(deletedEventAccount);
        }

        private async Task AddBadgesToAccount(Account account)
        {
            var badges = await _badgeRepository.GetBadgesByAccountId(account.Id);
            foreach (var badge in badges)
            {
                if (account.Points >= badge.Pointstreshold_User)
                {
                    await _badgeAccountRepository.Insert(new BadgeAccount { IdAccount = account.Id, IdBadge = badge.IdBadge });
                }
            }
            await _unitOfWork.Commit();
        }

        private async Task CheckNumberOfEventsMembers(CreateEventAccountCommand request)
        {
            var currentEvent = await _eventRepository.GetById(request.IdEvent);
            var eventAccounts = await _eventAccountRepository.GetEventAccountsByEventId(request.IdEvent);
            int total = currentEvent.EventMemeberLimit + (currentEvent.EventMemeberLimit + (int)(currentEvent.OverSale / currentEvent.EventMemeberLimit));
            if (eventAccounts.Count > total)
            {
                throw new ToMuchMembersException("The limit of participants of the event has been reached.You cannot join in.");
            }
        }

        private async Task AddPointsToAccount(AccountEvent accountEvent, bool ifPartcipantPresent, bool ifNew = false)
        {
            if (ifPartcipantPresent)
            {
                accountEvent.AccountNavigation.Points += POINTS_FOR_EVENT;
                await _eventAccountRepository.Update(accountEvent);
                await _unitOfWork.Commit();
            }
            else if (!ifPartcipantPresent && !ifNew)
            {
                accountEvent.AccountNavigation.Points = (accountEvent.AccountNavigation.Points - POINTS_FOR_EVENT) <= 0 ?
                    0 :
                    accountEvent.AccountNavigation.Points -= POINTS_FOR_EVENT;
            }
            await _eventAccountRepository.Update(accountEvent);
            await _unitOfWork.Commit();
        }
    }
}
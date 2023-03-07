using Application.Cqrs.Commands.BadgeAccount;
using Application.Cqrs.Queries.BadgeAccount;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class BadgeAccountHandler :
        IRequestHandler<GetBadgesAccountsQuery, List<BasicBadgeAccountDTO>>,
        IRequestHandler<GetAccountBadgesByAccountIdQuery, List<BasicBadgeAccountDTO>>,
        IRequestHandler<GetBadgeAccountByIdQuery, BasicBadgeAccountDTO>,
        IRequestHandler<CreateBadgeAccountCommand, BasicBadgeAccountDTO>,
        IRequestHandler<DeleteBadgeAccountCommand, BasicBadgeAccountDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBadgeAccountRepository _badgeAccountRepository;
        private readonly IMapper _mappper;

        public BadgeAccountHandler(IUnitOfWork unitOfWork, IBadgeAccountRepository badgeAccountRepository, IMapper mappper)
        {
            _unitOfWork = unitOfWork;
            _badgeAccountRepository = badgeAccountRepository;
            _mappper = mappper;
        }

        public async Task<List<BasicBadgeAccountDTO>> Handle(GetBadgesAccountsQuery request, CancellationToken cancellationToken)
        {
            var badgesAccounts = await _badgeAccountRepository.GetAll();
            var badgesAccountsResponse = _mappper.Map<List<BasicBadgeAccountDTO>>(badgesAccounts);
            return badgesAccountsResponse;
        }

        public async Task<BasicBadgeAccountDTO> Handle(GetBadgeAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var badgeAccount = await _badgeAccountRepository.GetById(request.IdBadge, request.IdAccount);
            if (badgeAccount is null)
            {
                throw new NotFoundRecordException($"There is no connection between badge and account with given account Id: {request.IdAccount}.");
            }
            return _mappper.Map<BasicBadgeAccountDTO>(badgeAccount);
        }

        public async Task<BasicBadgeAccountDTO> Handle(CreateBadgeAccountCommand request, CancellationToken cancellationToken)
        {
            var newBadgeAccount = await _badgeAccountRepository.Insert(_mappper.Map<BadgeAccount>(request));
            await _unitOfWork.Commit();
            var newBadgeAccountResponse = _mappper.Map<BasicBadgeAccountDTO>(newBadgeAccount);
            return newBadgeAccountResponse;
        }

        public async Task<BasicBadgeAccountDTO> Handle(DeleteBadgeAccountCommand request, CancellationToken cancellationToken)
        {
            var deletedBadgeAccount = await _badgeAccountRepository.Delete(request.IdAccount, request.IdBadge);
            if (deletedBadgeAccount is null)
            {
                throw new NotFoundRecordException($"There is no connection between badge and account with given account Id: {request.IdAccount} and badge Id: {request.IdBadge}.");
            }
            await _unitOfWork.Commit();
            var deletedBadgeAccountResponse = _mappper.Map<BasicBadgeAccountDTO>(deletedBadgeAccount);
            return deletedBadgeAccountResponse;
        }

        public async Task<List<BasicBadgeAccountDTO>> Handle(GetAccountBadgesByAccountIdQuery request, CancellationToken cancellationToken)
        {
            var accountBadges = await _badgeAccountRepository.GetAccountBadgesByAccountId(request.IdAccount);
            return _mappper.Map<List<BasicBadgeAccountDTO>>(accountBadges);
        }
    }
}
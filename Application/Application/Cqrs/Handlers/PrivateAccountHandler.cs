using Application.Cqrs.Commands.PrivateAccount;
using Application.Cqrs.Queries.PrivateAccounts;
using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class PrivateAccountHandler :
        IRequestHandler<CreatePrivateAccountCommand, BasicAccountDTO>,
        IRequestHandler<GetPrivateAccountsQuery, List<BasicAccountDTO>>,
        IRequestHandler<GetTopPrivateAccountsWithMostBadgePointsQuery, List<PrivateAccountWithBadgePointsDTO>>,
        IRequestHandler<GetPrivateAccountQuery, BasicAccountDTO>,
        IRequestHandler<DeletePrivateAccountCommand, BasicAccountDTO>,
        IRequestHandler<UpdatePrivateAccountCommand, BasicAccountDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IPrivateAccountRepository _privateAccountRepository;
        private readonly IMapper _mapper;

        public PrivateAccountHandler(IUnitOfWork unitOfWork, IAccountRepository accountRepository, IPrivateAccountRepository privateAccountRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _privateAccountRepository = privateAccountRepository;
        }

        public async Task<BasicAccountDTO> Handle(CreatePrivateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.Insert(_mapper.Map<Account>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicAccountDTO>(account);
        }

        public async Task<List<BasicAccountDTO>> Handle(GetPrivateAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _privateAccountRepository.GetAll();
            var privateAccountsResponse = _mapper.Map<List<BasicAccountDTO>>(accounts);
            return privateAccountsResponse;
        }

        public async Task<BasicAccountDTO> Handle(GetPrivateAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await _privateAccountRepository.GetById(request.Id);
            if (account is null)
            {
                throw new NotFoundRecordException($"There is no private account with given Id: {request.Id}.");
            }
            return _mapper.Map<BasicAccountDTO>(account);
        }

        public async Task<BasicAccountDTO> Handle(DeletePrivateAccountCommand request, CancellationToken cancellationToken)
        {
            var deletedPrivateAccount = await _accountRepository.Delete(request.IdAccount);
            if (deletedPrivateAccount is null)
            {
                throw new NotFoundRecordException($"There is no private account with given Id: {request.IdAccount}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicAccountDTO>(deletedPrivateAccount);
        }

        public async Task<BasicAccountDTO> Handle(UpdatePrivateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountToUpdate = await _accountRepository.GetAccountWithRelationalEntitiesAsync(request.IdAccount);
            if (accountToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no private account with given Id: {request.IdAccount}.");
            }

            if (request.Email is not null)
            {
                accountToUpdate.Email = request.Email;
            }
            if (request.Phone is not null)
            {
                accountToUpdate.PhoneNumber = request.Phone;
            }
            if (request.FirstName is not null)
            {
                accountToUpdate.PrivateAccountNavigation.FirstName = request.FirstName;
            }
            if (request.LastName is not null)
            {
                accountToUpdate.PrivateAccountNavigation.LastName = request.LastName;
            }

            accountToUpdate.VerificationStatus = request.VerificationStatus;
            accountToUpdate.Base64dataPicture = request.Base64dataPicture;
            accountToUpdate.GoldSponsorBadge = request.GoldSponsorBadge;
            accountToUpdate.PrivateAccountNavigation.BirthDate = request.BirthDate;

            await _accountRepository.Update(accountToUpdate);
            await _unitOfWork.Commit();

            return _mapper.Map<BasicAccountDTO>(accountToUpdate);
        }

        public async Task<List<PrivateAccountWithBadgePointsDTO>> Handle(GetTopPrivateAccountsWithMostBadgePointsQuery request, CancellationToken cancellationToken)
        {
            return await _privateAccountRepository.GetTopPrivateAccountsWithBadgePoints(request.NumberOfPeople);
        }
    }
}
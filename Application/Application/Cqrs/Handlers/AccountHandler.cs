using Application.Cqrs.Commands.Account;
using Application.Cqrs.Queries.Account;
using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ExtendedDtos.Responses;
using Application.Dtos.ServiceDtos.Requests;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Application.Providers;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Cqrs.Handlers
{
    public class AccountHandler :
        IRequestHandler<GetAllAccountQuery, List<BasicAccountDTO>>,
        IRequestHandler<GetAccountsByEventIdQuery, List<BasicAccountDTO>>,
        IRequestHandler<GetAccountsByGroupIdQuery, List<BasicAccountDTO>>,
        IRequestHandler<GetUnConfirmedAccountsByEventId, List<BasicAccountDTO>>,
        IRequestHandler<GetTopAccountsWithMostCreatedEventsQuery, List<AccountsWithMostCreatedEventsDTO>>,
        IRequestHandler<CreateAccountCommand, BasicAccountDTO>,
        IRequestHandler<GetAccountByIdQuery, BasicAccountDTO>,
        IRequestHandler<GetAccountByEmailQuery, BasicAccountDTO>,
        IRequestHandler<UpdateAccountCommand, BasicAccountDTO>,
        IRequestHandler<DeleteAccountCommand, BasicAccountDTO>,
        IRequestHandler<UpdatePasswordAccountCommand, BasicAccountDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<Account> _userManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountHandler(IUnitOfWork unitOfWork, IAccountRepository accountRepository, UserManager<Account> userManager, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<List<BasicAccountDTO>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAll();
            var accountResponses = _mapper.Map<List<BasicAccountDTO>>(accounts);
            return accountResponses;
        }

        public async Task<BasicAccountDTO> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var newAccount = await _accountRepository.Insert(_mapper.Map<Account>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicAccountDTO>(newAccount);
        }

        public async Task<BasicAccountDTO> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountWithRelationalEntitiesAsync(request.Id);
            if (account is null)
            {
                throw new NotFoundRecordException($"There is no account with given Id: {request.Id}.");
            }
            var accountResponse = _mapper.Map<BasicAccountDTO>(account);
            return accountResponse;
        }

        public async Task<BasicAccountDTO> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountToUpdate = await _accountRepository.GetAccountWithRelationalEntitiesAsync(request.IdAccount);
            if (accountToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no account with given Id: {request.IdAccount}.");
            }
            if (request.Email is not null)
            {
                accountToUpdate.Email = request.Email;
            }
            if (request.Phone is not null)
            {
                accountToUpdate.PhoneNumber = request.Phone;
            }
            accountToUpdate.VerificationStatus = request.VerificationStatus;
            accountToUpdate.GoldSponsorBadge = request.GoldSponsorBadge;
            accountToUpdate.Base64dataPicture = request.Base64dataPicture;

            if (request.PrivateAccount is not null)
            {
                UpdatePrivateAccount(accountToUpdate, request);
            }
            else if (request.CompanyAccount is not null)
            {
                UpdateCompanyAccount(accountToUpdate, request);
            }

            await _accountRepository.Update(accountToUpdate);
            await _unitOfWork.Commit();
            return _mapper.Map<BasicAccountDTO>(accountToUpdate);
        }

        private void UpdatePrivateAccount(Account account, UpdateAccountCommand request)
        {
            if (request.PrivateAccount.FirstName is not null)
            {
                account.PrivateAccountNavigation.FirstName = request.PrivateAccount.FirstName;
            }
            if (request.PrivateAccount.LastName is not null)
            {
                account.PrivateAccountNavigation.LastName = request.PrivateAccount.LastName;
            }
            account.PrivateAccountNavigation.BirthDate = request.PrivateAccount.BirthDate;
        }

        private void UpdateCompanyAccount(Account account, UpdateAccountCommand request)
        {
            if (request.CompanyAccount.Name is not null)
            {
                account.CompanyAccountNavigation.Name = request.CompanyAccount.Name;
            }
            if (request.CompanyAccount.CompanyDesc is not null)
            {
                account.CompanyAccountNavigation.CompanyDesc = request.CompanyAccount.CompanyDesc;
            }
            if (request.CompanyAccount.Krs is not null)
            {
                account.CompanyAccountNavigation.Krs = request.CompanyAccount.Krs;
            }
            if (request.CompanyAccount.Nip is not null)
            {
                account.CompanyAccountNavigation.Nip = request.CompanyAccount.Nip;
            }
            if (request.CompanyAccount.BankAccount is not null)
            {
                account.CompanyAccountNavigation.BankAccount = request.CompanyAccount.BankAccount;
            }
            if (request.CompanyAccount.CompanyWebsiteLink is not null)
            {
                account.CompanyAccountNavigation.CompanyWebsiteLink = request.CompanyAccount.CompanyWebsiteLink;
            }

            if (request.CompanyAccount.CompanyRepresentative is not null)
            {
                UpdateComapnyRepresentative(account, request);
            }

            if (request.CompanyAccount.CompanyAddress is not null)
            {
                UpdateCompanyAddress(account, request);
            }
        }

        private void UpdateComapnyRepresentative(Account account, UpdateAccountCommand request)
        {
            account.CompanyAccountNavigation.ComapnyRepresentativeNavigation.FirstName = request.CompanyAccount.CompanyRepresentative.FirstName;
            account.CompanyAccountNavigation.ComapnyRepresentativeNavigation.LastName = request.CompanyAccount.CompanyRepresentative.LastName;
            account.CompanyAccountNavigation.ComapnyRepresentativeNavigation.RepresentativeMail = request.CompanyAccount.CompanyRepresentative.RepresentativeMail;
            account.CompanyAccountNavigation.ComapnyRepresentativeNavigation.RepresentativePhone = request.CompanyAccount.CompanyRepresentative.RepresentativePhone;
        }

        private void UpdateCompanyAddress(Account account, UpdateAccountCommand request)
        {
            account.CompanyAccountNavigation.CompanyAddressNavigation.Street = request.CompanyAccount.CompanyAddress.Street;
            account.CompanyAccountNavigation.CompanyAddressNavigation.BuildingNumber = request.CompanyAccount.CompanyAddress.BuildingNumber;
            account.CompanyAccountNavigation.CompanyAddressNavigation.ApartmentNumber = request.CompanyAccount.CompanyAddress.ApartmentNumber;
            account.CompanyAccountNavigation.CompanyAddressNavigation.ZipCode = request.CompanyAccount.CompanyAddress.ZipCode;
            account.CompanyAccountNavigation.CompanyAddressNavigation.City = request.CompanyAccount.CompanyAddress.City;
            account.CompanyAccountNavigation.CompanyAddressNavigation.Province = request.CompanyAccount.CompanyAddress.Province;
            account.CompanyAccountNavigation.CompanyAddressNavigation.Country = request.CompanyAccount.CompanyAddress.Country;
        }

        public async Task<BasicAccountDTO> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var accountToDelete = await _accountRepository.Delete(request.IdAccount);
            if (accountToDelete is null)
            {
                throw new NotFoundRecordException($"There is no account with given Id: {request.IdAccount}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicAccountDTO>(accountToDelete);
        }

        public async Task<BasicAccountDTO> Handle(UpdatePasswordAccountCommand request, CancellationToken cancellationToken)
        {
            var accountToUpdate = await _userManager.FindByEmailAsync(request.Email);
            if (accountToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no account with given Email: {request.Email}.");
            }
            if (request.NewPassword is not null)
            {
                accountToUpdate.PasswordHash = _userManager.PasswordHasher.HashPassword(accountToUpdate, request.NewPassword);
            }
            var result = await _userManager.UpdateAsync(accountToUpdate);
            if (result.Succeeded)
            {
                await _emailService.SendEmailAsync(HTMLProvider.ProvideHtmlEmailTemaplate(new EmailRequestDTO
                {
                    FirstName = accountToUpdate.PrivateAccountNavigation.FirstName,
                    LastName = accountToUpdate.PrivateAccountNavigation.LastName,
                    Subject = "Restore Password Confirmation",
                    To = accountToUpdate.Email
                }, "PasswordRecovery"));
            }
            return _mapper.Map<BasicAccountDTO>(accountToUpdate);
        }

        public async Task<List<AccountsWithMostCreatedEventsDTO>> Handle(GetTopAccountsWithMostCreatedEventsQuery request, CancellationToken cancellationToken)
        {
            return await _accountRepository.GetAccountsWithMostCreatedEvents(request.NumberOfEvents);
        }

        public async Task<List<BasicAccountDTO>> Handle(GetAccountsByEventIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicAccountDTO>>(await _accountRepository.GetRelatedAccountsByEventId(request.IdEvent));
        }

        public async Task<List<BasicAccountDTO>> Handle(GetAccountsByGroupIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicAccountDTO>>(await _accountRepository.GetRelatedAccountsByGroupId(request.IdGroup));
        }

        public async Task<BasicAccountDTO> Handle(GetAccountByEmailQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetBy(x => x.Email == request.Email);
            if (account is null)
            {
                throw new NotFoundRecordException($"There is no account with given Email: {request.Email}.");
            }
            return _mapper.Map<BasicAccountDTO>(account);
        }

        public async Task<List<BasicAccountDTO>> Handle(GetUnConfirmedAccountsByEventId request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetUnconfirmedAccountsByEventId(request.IdEvent);
            return _mapper.Map<List<BasicAccountDTO>>(accounts);
        }
    }
}
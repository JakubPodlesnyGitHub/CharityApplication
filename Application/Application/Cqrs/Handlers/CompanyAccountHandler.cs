using Application.Cqrs.Commands.CompanyAccount;
using Application.Cqrs.Queries.CompanyAccount;
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
    public class CompanyAccountHandler :
        IRequestHandler<GetCompanyAccountsQuery, List<BasicAccountDTO>>,
        IRequestHandler<GetTopCompanyAccountsWithMostBadgePointsQuery, List<CompanyAccountWithBadgePointsDTO>>,
        IRequestHandler<GetCompanyAccountByIdQuery, BasicAccountDTO>,
        IRequestHandler<CreateCompanyAccountCommand, BasicAccountDTO>,
        IRequestHandler<DeleteCompanyAccountCommand, BasicAccountDTO>,
        IRequestHandler<UpdateCompanyAccountCommand, BasicAccountDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyAccountRepository _companyAccountRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public CompanyAccountHandler(IUnitOfWork unitOfWork, ICompanyAccountRepository companyAccountRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _companyAccountRepository = companyAccountRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicAccountDTO>> Handle(GetCompanyAccountsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicAccountDTO>>(await _companyAccountRepository.GetAll());
        }

        public async Task<BasicAccountDTO> Handle(GetCompanyAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var companyAccount = await _companyAccountRepository.GetById(request.Id);
            if (companyAccount is null)
            {
                throw new NotFoundRecordException($"There is no company account with given Id: {request.Id}.");
            }
            return _mapper.Map<BasicAccountDTO>(companyAccount);
        }

        public async Task<BasicAccountDTO> Handle(CreateCompanyAccountCommand request, CancellationToken cancellationToken)
        {
            var newAccount = await _accountRepository.Insert(_mapper.Map<Account>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicAccountDTO>(newAccount);
        }

        public async Task<BasicAccountDTO> Handle(DeleteCompanyAccountCommand request, CancellationToken cancellationToken)
        {
            var deletedAccount = await _accountRepository.Delete(request.IdAccount);
            if (deletedAccount is null)
            {
                throw new NotFoundRecordException($"There is no company account with given Id: {request.IdAccount}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicAccountDTO>(deletedAccount);
        }

        public async Task<BasicAccountDTO> Handle(UpdateCompanyAccountCommand request, CancellationToken cancellationToken)
        {
            var accountToUpdate = await _accountRepository.GetAccountWithRelationalEntitiesAsync(request.IdAccount);
            if (accountToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no company account with given Id: {request.IdAccount}.");
            }
            if (request.Name is not null)
            {
                accountToUpdate.CompanyAccountNavigation.Name = request.Name;
            }
            if (request.CompanyDesc is not null)
            {
                accountToUpdate.CompanyAccountNavigation.CompanyDesc = request.CompanyDesc;
            }
            if (request.Krs is not null)
            {
                accountToUpdate.CompanyAccountNavigation.Krs = request.Krs;
            }
            if (request.Nip is not null)
            {
                accountToUpdate.CompanyAccountNavigation.Nip = request.Nip;
            }
            if (request.BankAccount is not null)
            {
                accountToUpdate.CompanyAccountNavigation.BankAccount = request.BankAccount;
            }
            if (request.CompanyWebsiteLink is not null)
            {
                accountToUpdate.CompanyAccountNavigation.CompanyWebsiteLink = request.CompanyWebsiteLink;
            }

            if (request.CompanyRepresentative is not null)
            {
                accountToUpdate.CompanyAccountNavigation.ComapnyRepresentativeNavigation.FirstName = request.CompanyRepresentative.FirstName;
                accountToUpdate.CompanyAccountNavigation.ComapnyRepresentativeNavigation.LastName = request.CompanyRepresentative.LastName;
                accountToUpdate.CompanyAccountNavigation.ComapnyRepresentativeNavigation.RepresentativeMail = request.CompanyRepresentative.RepresentativeMail;
                accountToUpdate.CompanyAccountNavigation.ComapnyRepresentativeNavigation.RepresentativePhone = request.CompanyRepresentative.RepresentativePhone;
            }

            if (request.CompanyAddress is not null)
            {
                accountToUpdate.CompanyAccountNavigation.CompanyAddressNavigation.Street = request.CompanyAddress.Street;
                accountToUpdate.CompanyAccountNavigation.CompanyAddressNavigation.BuildingNumber = request.CompanyAddress.BuildingNumber;
                accountToUpdate.CompanyAccountNavigation.CompanyAddressNavigation.ApartmentNumber = request.CompanyAddress.ApartmentNumber;
                accountToUpdate.CompanyAccountNavigation.CompanyAddressNavigation.ZipCode = request.CompanyAddress.ZipCode;
                accountToUpdate.CompanyAccountNavigation.CompanyAddressNavigation.City = request.CompanyAddress.City;
                accountToUpdate.CompanyAccountNavigation.CompanyAddressNavigation.Province = request.CompanyAddress.Province;
                accountToUpdate.CompanyAccountNavigation.CompanyAddressNavigation.Country = request.CompanyAddress.Country;
            }

            await _accountRepository.Update(accountToUpdate);
            await _unitOfWork.Commit();

            return _mapper.Map<BasicAccountDTO>(accountToUpdate);
        }

        public async Task<List<CompanyAccountWithBadgePointsDTO>> Handle(GetTopCompanyAccountsWithMostBadgePointsQuery request, CancellationToken cancellationToken)
        {
            return await _companyAccountRepository.GetTopCompanyAccountsWithBadgePoints(request.NumberOfCompanyAccounts);
        }
    }
}
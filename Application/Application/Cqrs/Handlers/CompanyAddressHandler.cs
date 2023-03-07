using Application.Cqrs.Commands.CompanyAddress;
using Application.Cqrs.Queries.ComapnyAddress;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class CompanyAddressHandler :
        IRequestHandler<GetCompaniesAddressesQuery, List<BasicCompanyAddressDTO>>,
        IRequestHandler<GetCompanyAddressByIdQuery, BasicCompanyAddressDTO>,
        IRequestHandler<CreateCompanyAddressCommand, BasicCompanyAddressDTO>,
        IRequestHandler<DeleteCompanyAddressCommand, BasicCompanyAddressDTO>,
        IRequestHandler<UpdateCompanyAddressCommand, BasicCompanyAddressDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyAddressRepository _companyAddressRepository;
        private readonly IMapper _mapper;

        public CompanyAddressHandler(IUnitOfWork unitOfWork, ICompanyAddressRepository comapnyAddressRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _companyAddressRepository = comapnyAddressRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicCompanyAddressDTO>> Handle(GetCompaniesAddressesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicCompanyAddressDTO>>(await _companyAddressRepository.GetAll());
        }

        public async Task<BasicCompanyAddressDTO> Handle(GetCompanyAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var companyAddress = await _companyAddressRepository.GetById(request.Id);
            if (companyAddress is null)
            {
                throw new NotFoundRecordException($"There is no company address with given Id: {request.Id}.");
            }
            return _mapper.Map<BasicCompanyAddressDTO>(companyAddress);
        }

        public async Task<BasicCompanyAddressDTO> Handle(CreateCompanyAddressCommand request, CancellationToken cancellationToken)
        {
            var newCompanyAddress = await _companyAddressRepository.Insert(_mapper.Map<CompanyAddress>(request));
            await _unitOfWork.Commit();
            var companyAddressResponse = _mapper.Map<BasicCompanyAddressDTO>(newCompanyAddress);
            return companyAddressResponse;
        }

        public async Task<BasicCompanyAddressDTO> Handle(DeleteCompanyAddressCommand request, CancellationToken cancellationToken)
        {
            var deletedCompanyAddress = await _companyAddressRepository.Delete(request.IdCompanyAddress);
            if (deletedCompanyAddress is null)
            {
                throw new NotFoundRecordException($"There is no company address with given Id: {request.IdCompanyAddress}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicCompanyAddressDTO>(deletedCompanyAddress);
        }

        public async Task<BasicCompanyAddressDTO> Handle(UpdateCompanyAddressCommand request, CancellationToken cancellationToken)
        {
            var companyAddressToUpdate = await _companyAddressRepository.GetById(request.IdCompanyAddress);
            if (companyAddressToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no company address with given Id: {request.IdCompanyAddress}.");
            }
            if (request.Street is not null)
            {
                companyAddressToUpdate.Street = request.Street;
            }
            if (companyAddressToUpdate.BuildingNumber != request.BuildingNumber)
            {
                companyAddressToUpdate.BuildingNumber = request.BuildingNumber;
            }
            if (request.ApartmentNumber != companyAddressToUpdate.ApartmentNumber)
            {
                companyAddressToUpdate.ApartmentNumber = request.ApartmentNumber;
            }
            if (request.ZipCode is not null)
            {
                companyAddressToUpdate.ZipCode = request.ZipCode;
            }
            if (request.City is not null)
            {
                companyAddressToUpdate.City = request.City;
            }
            if (request.Province is not null)
            {
                companyAddressToUpdate.Province = request.Province;
            }
            if (request.Country is not null)
            {
                companyAddressToUpdate.Country = request.Country;
            }
            await _companyAddressRepository.Update(companyAddressToUpdate);
            await _unitOfWork.Commit();
            return _mapper.Map<BasicCompanyAddressDTO>(companyAddressToUpdate);
        }
    }
}
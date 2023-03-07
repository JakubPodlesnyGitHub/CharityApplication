using Application.Cqrs.Commands.CompanyRepresentative;
using Application.Cqrs.Queries.CompanyRepresentative;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class CompanyRepresentativeHandler :
        IRequestHandler<GetCompaniesRepresentativesQuery, List<BasicCompanyRepresentativeDTO>>,
        IRequestHandler<GetComapnyRepresentativeByIdQuery, BasicCompanyRepresentativeDTO>,
        IRequestHandler<CreateCompanyRepresentativeCommand, BasicCompanyRepresentativeDTO>,
        IRequestHandler<DeleteCompanyRepresentativeCommand, BasicCompanyRepresentativeDTO>,
        IRequestHandler<UpdateCompanyRepresentativeCommand, BasicCompanyRepresentativeDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepresentativeRepository _companyRepresentativeRepository;
        private readonly IMapper _mapper;

        public CompanyRepresentativeHandler(IUnitOfWork unitOfWork, ICompanyRepresentativeRepository companyRepresentativeRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _companyRepresentativeRepository = companyRepresentativeRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicCompanyRepresentativeDTO>> Handle(GetCompaniesRepresentativesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicCompanyRepresentativeDTO>>(await _companyRepresentativeRepository.GetAll());
        }

        public async Task<BasicCompanyRepresentativeDTO> Handle(CreateCompanyRepresentativeCommand request, CancellationToken cancellationToken)
        {
            var newCompanyRepresentative = await _companyRepresentativeRepository.Insert(_mapper.Map<CompanyRepresentative>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicCompanyRepresentativeDTO>(newCompanyRepresentative);
        }

        public async Task<BasicCompanyRepresentativeDTO> Handle(GetComapnyRepresentativeByIdQuery request, CancellationToken cancellationToken)
        {
            var companyRepresentative = await _companyRepresentativeRepository.GetById(request.Id);
            if (companyRepresentative is null)
            {
                throw new NotFoundRecordException($"There is no company representative with given Id: {request.Id}.");
            }
            return _mapper.Map<BasicCompanyRepresentativeDTO>(companyRepresentative);
        }

        public async Task<BasicCompanyRepresentativeDTO> Handle(DeleteCompanyRepresentativeCommand request, CancellationToken cancellationToken)
        {
            var deletedCompanyRepresentative = await _companyRepresentativeRepository.Delete(request.IdCompanyRepresentative);
            if (deletedCompanyRepresentative is null)
            {
                throw new NotFoundRecordException($"There is no company representative with given Id: {request.IdCompanyRepresentative}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicCompanyRepresentativeDTO>(deletedCompanyRepresentative);
        }

        public async Task<BasicCompanyRepresentativeDTO> Handle(UpdateCompanyRepresentativeCommand request, CancellationToken cancellationToken)
        {
            var companyRepresentativeToUpdate = await _companyRepresentativeRepository.GetById(request.IdCompanyRepresentative);
            if (companyRepresentativeToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no company representative with given Id: {request.IdCompanyRepresentative}.");
            }
            if (request.FirstName is not null)
            {
                companyRepresentativeToUpdate.FirstName = request.FirstName;
            }
            if (request.LastName is not null)
            {
                companyRepresentativeToUpdate.LastName = request.LastName;
            }
            if (request.RepresentativeMail is not null)
            {
                companyRepresentativeToUpdate.RepresentativeMail = request.RepresentativeMail;
            }
            if (request.RepresentativePhone is not null)
            {
                companyRepresentativeToUpdate.RepresentativePhone = request.RepresentativePhone;
            }
            await _companyRepresentativeRepository.Update(companyRepresentativeToUpdate);
            await _unitOfWork.Commit();
            return _mapper.Map<BasicCompanyRepresentativeDTO>(companyRepresentativeToUpdate);
        }
    }
}
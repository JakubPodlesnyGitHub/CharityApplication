using Application.Cqrs.Commands.AssesmentForm;
using Application.Cqrs.Queries.AssesmentForm;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class AssesmentFormHandler :
        IRequestHandler<GetAssesmentFormsQuery, List<BasicAssesmentFormDTO>>,
        IRequestHandler<GetAssesmentFormsByEventIdQuery, List<BasicAssesmentFormDTO>>,
        IRequestHandler<GetAssesmentFormByIdQuery, BasicAssesmentFormDTO>,
        IRequestHandler<CreateAssesmentFormCommand, BasicAssesmentFormDTO>,
        IRequestHandler<DeleteAssesmentFormCommand, BasicAssesmentFormDTO>,
        IRequestHandler<UpdateAssesmentFormCommand, BasicAssesmentFormDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAssesmentFormRepository _assesmentFormRepository;
        private readonly IMapper _mapper;

        public AssesmentFormHandler(IUnitOfWork unitOfWork, IAssesmentFormRepository assesmentFormRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _assesmentFormRepository = assesmentFormRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicAssesmentFormDTO>> Handle(GetAssesmentFormsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicAssesmentFormDTO>>(await _assesmentFormRepository.GetAll());
        }

        public async Task<BasicAssesmentFormDTO> Handle(GetAssesmentFormByIdQuery request, CancellationToken cancellationToken)
        {
            var assesmentForm = await _assesmentFormRepository.GetById(request.Id);
            if (assesmentForm is null)
            {
                throw new NotFoundRecordException($"There is no assemeent form with given Id: {request.Id}.");
            }
            return _mapper.Map<BasicAssesmentFormDTO>(assesmentForm);
        }

        public async Task<BasicAssesmentFormDTO> Handle(CreateAssesmentFormCommand request, CancellationToken cancellationToken)
        {
            var newAssesmentForm = await _assesmentFormRepository.Insert(_mapper.Map<AssesmentForm>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicAssesmentFormDTO>(newAssesmentForm);
        }

        public async Task<BasicAssesmentFormDTO> Handle(DeleteAssesmentFormCommand request, CancellationToken cancellationToken)
        {
            var deletedAssesmentResponse = await _assesmentFormRepository.Delete(request.IdAssesmentForm);
            if (deletedAssesmentResponse is null)
            {
                throw new NotFoundRecordException($"There is no assemeent form with given Id: {request.IdAssesmentForm}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicAssesmentFormDTO>(deletedAssesmentResponse);
        }

        public async Task<BasicAssesmentFormDTO> Handle(UpdateAssesmentFormCommand request, CancellationToken cancellationToken)
        {
            var assesmentFormToUpdate = await _assesmentFormRepository.GetById(request.IdAssesmentForm);
            if (assesmentFormToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no assemeent form with given Id: {request.IdAssesmentForm}.");
            }
            if (request.Mail is not null)
            {
                assesmentFormToUpdate.Mail = request.Mail;
            }
            if (request.Subject is not null)
            {
                assesmentFormToUpdate.Subject = request.Subject;
            }
            if (request.Message is not null)
            {
                assesmentFormToUpdate.Message = request.Message;
            }
            if (assesmentFormToUpdate.EventRate != request.EventRate)
            {
                assesmentFormToUpdate.EventRate = request.EventRate;
            }
            if (assesmentFormToUpdate.AppRate != request.AppRate)
            {
                assesmentFormToUpdate.AppRate = request.AppRate;
            }
            if (assesmentFormToUpdate.IdEvent != request.Event)
            {
                assesmentFormToUpdate.IdEvent = request.Event;
            }
            await _assesmentFormRepository.Update(assesmentFormToUpdate);
            await _unitOfWork.Commit();
            return _mapper.Map<BasicAssesmentFormDTO>(assesmentFormToUpdate);
        }

        public Task<List<BasicAssesmentFormDTO>> Handle(GetAssesmentFormsByEventIdQuery request, CancellationToken cancellationToken)
        {
            return _assesmentFormRepository.GetAssessmentFormsByEventId(request.IdEvent);
        }
    }
}
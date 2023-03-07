using Application.Cqrs.Commands.ContactForm;
using Application.Cqrs.Queries.ContactForm;
using Application.Dtos.BasicDtos.Responses;
using Application.Dtos.ServiceDtos.Requests;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Application.Providers;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class ContactFormHandler :
        IRequestHandler<CreateContactFormCommand, BasicContactFormDTO>,
        IRequestHandler<DeleteContactFormCommand, BasicContactFormDTO>,
        IRequestHandler<GetAllContactFormsQuery, List<BasicContactFormDTO>>,
        IRequestHandler<GetContactFormByIdQuery, BasicContactFormDTO>,
        IRequestHandler<UpdateContactFormCommand, BasicContactFormDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactFormRepository _contactFormRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public ContactFormHandler(IUnitOfWork unitOfWork, IContactFormRepository contactFormRepository, IEmailService emailService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contactFormRepository = contactFormRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<BasicContactFormDTO> Handle(CreateContactFormCommand request, CancellationToken cancellationToken)
        {
            var contactForm = await _contactFormRepository.Insert(_mapper.Map<Domain.Entities.ContactForm>(request));
            await _unitOfWork.Commit();
            if (contactForm is not null)
            {
                await _emailService.SendEmailAsync(HTMLProvider.ProvideHtmlEmailTemaplate(new EmailRequestDTO
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Subject = "Contact Form Confirmation",
                    To = request.Mail
                }, "ContactForm"));
            }
            return _mapper.Map<BasicContactFormDTO>(contactForm);
        }

        public async Task<BasicContactFormDTO> Handle(DeleteContactFormCommand request, CancellationToken cancellationToken)
        {
            var deletedContactForm = await _contactFormRepository.Delete(request.IdContactForm);
            if (deletedContactForm is null)
            {
                throw new NotFoundRecordException($"There is no contect form with given Id: {request.IdContactForm}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicContactFormDTO>(deletedContactForm);
        }

        public async Task<List<BasicContactFormDTO>> Handle(GetAllContactFormsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicContactFormDTO>>(await _contactFormRepository.GetAll());
        }

        public async Task<BasicContactFormDTO> Handle(GetContactFormByIdQuery request, CancellationToken cancellationToken)
        {
            var contactForm = await _contactFormRepository.GetById(request.Id);
            if (contactForm is null)
            {
                throw new NotFoundRecordException($"There is no contect form with given Id: {request.Id}.");
            }
            return _mapper.Map<BasicContactFormDTO>(contactForm);
        }

        public async Task<BasicContactFormDTO> Handle(UpdateContactFormCommand request, CancellationToken cancellationToken)
        {
            var contactFormToUpdate = await _contactFormRepository.GetById(request.IdContactForm);
            if (contactFormToUpdate is null)
            {
                throw new NotFoundRecordException($"There is no contect form with given Id: {request.IdContactForm}.");
            }
            if (request.FirstName is not null)
            {
                contactFormToUpdate.FirstName = request.FirstName;
            }
            if (request.LastName is not null)
            {
                contactFormToUpdate.LastName = request.LastName;
            }
            if (request.Subject is not null)
            {
                contactFormToUpdate.Subject = request.Subject;
            }
            if (request.Message is not null)
            {
                contactFormToUpdate.Message = request.Message;
            }
            if (request.Mail is not null)
            {
                contactFormToUpdate.Mail = request.Mail;
            }

            await _contactFormRepository.Update(contactFormToUpdate);
            await _unitOfWork.Commit();

            return _mapper.Map<BasicContactFormDTO>(contactFormToUpdate);
        }
    }
}
using Application.Cqrs.Commands.GroupAccount;
using Application.Cqrs.Queries.GroupAccount;
using Application.Dtos.BasicDtos.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Cqrs.Handlers
{
    public class GroupAccountHandler :
        IRequestHandler<GetGroupsAccountsQuery, List<BasicGroupAccountDTO>>,
        IRequestHandler<GetGroupAccountsByIdQuery, List<BasicGroupAccountDTO>>,
        IRequestHandler<GetGroupAccountByIdQuery, BasicGroupAccountDTO>,
        IRequestHandler<CreateGroupAccountCommand, BasicGroupAccountDTO>,
        IRequestHandler<DeleteGroupAccountCommand, BasicGroupAccountDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupAccountRepository _groupAccountRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupAccountHandler(IUnitOfWork unitOfWork, IGroupAccountRepository groupAccountRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _groupAccountRepository = groupAccountRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<List<BasicGroupAccountDTO>> Handle(GetGroupsAccountsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupAccountDTO>>(await _groupAccountRepository.GetAll());
        }

        public async Task<List<BasicGroupAccountDTO>> Handle(GetGroupAccountsByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BasicGroupAccountDTO>>(await _groupAccountRepository.GetGroupAccountsByGroupId(request.IdGroup));
        }

        public async Task<BasicGroupAccountDTO> Handle(GetGroupAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var groupAccount = await _groupAccountRepository.GetById(request.IdAccount, request.IdGroup);
            if (groupAccount is null)
            {
                throw new NotFoundRecordException($"There is no connection between group and account with given account Id: {request.IdAccount} and group Id: {request.IdGroup} .");
            }
            return _mapper.Map<BasicGroupAccountDTO>(groupAccount);
        }

        public async Task<BasicGroupAccountDTO> Handle(CreateGroupAccountCommand request, CancellationToken cancellationToken)
        {
            await CheckNumberOfGroupMembers(request);
            var newGroupAccount = await _groupAccountRepository.Insert(_mapper.Map<GroupAccount>(request));
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupAccountDTO>(newGroupAccount);
        }

        public async Task<BasicGroupAccountDTO> Handle(DeleteGroupAccountCommand request, CancellationToken cancellationToken)
        {
            var deletedGroupAccount = await _groupAccountRepository.Delete(request.IdAccount, request.IdGroup);
            if (deletedGroupAccount is null)
            {
                throw new NotFoundRecordException($"There is no connection between group and account with given account Id: {request.IdAccount} and group Id: {request.IdGroup}.");
            }
            await _unitOfWork.Commit();
            return _mapper.Map<BasicGroupAccountDTO>(deletedGroupAccount);
        }

        private async Task CheckNumberOfGroupMembers(CreateGroupAccountCommand request)
        {
            var currentGroup = await _groupRepository.GetById(request.IdGroup);
            var currentAccounts = await _groupAccountRepository.GetGroupAccountsByGroupId(request.IdGroup);
            if (currentGroup.NumberOfParticipants < currentAccounts.Count)
            {
                throw new ToMuchMembersException("The limit of participants of the group has been reached.You cannot join in.");
            }
        }
    }
}
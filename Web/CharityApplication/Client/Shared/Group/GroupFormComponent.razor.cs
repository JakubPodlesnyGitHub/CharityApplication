using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.AccountGroup;
using CharityApplication.Client.Model.Group;
using CharityApplication.Client.Shared.Dialog;
using CharityApplication.Client.Validators.Group;
using CharityApplication.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Shared.Group
{
    public partial class GroupFormComponent
    {
        public GroupModel Model { get; set; } = new GroupModel();
        public BasicAccountDTO LoggedUser { get; set; } = new BasicAccountDTO();
        public GroupModelValidator Validator { get; set; } = null!;
        public List<BasicGroupNameDTO> GroupNames { get; set; } = new List<BasicGroupNameDTO>();
        private MudForm? Form;

        [Parameter]
        public FormState FormState { get; set; }

        [Parameter]
        public int IdGroup { get; set; }

        [Inject]
        public IGroupNameRepository GroupNameRepository { get; set; }

        [Inject]
        public IGroupRepository GroupRepository { get; set; }

        [Inject]
        public IGroupAccountRepository GroupAccountRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private string FormTitle = string.Empty;
        private string ButtonTitle = string.Empty;
        private string SelectLabelGroupName = "Group Name";
        private string SelectLabelGroupType = "Group Type";

        protected override async Task OnInitializedAsync()
        {
            Validator = new GroupModelValidator();
            GroupNames = await GroupNameRepository.GetGroupNames();

            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                LoggedUser = JsonSerializer.Deserialize<BasicAccountDTO>(
                                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
            }

            if (FormState == FormState.CREATE)
            {
                FormTitle = "Group Creation";
                ButtonTitle = "Create Group";
                Model.IdGroupOwner = LoggedUser.IdAccount;
            }
            else
            {
                FormTitle = "Group Edition";
                ButtonTitle = "Update Group";
                var groupDTO = await GroupRepository.GetGroup(IdGroup);
                if (!string.IsNullOrEmpty(groupDTO.Detail))
                {
                    SnackBar.Add(groupDTO.Detail, Severity.Error);
                }
                else
                {
                    Model = ProvideGroupModelValues(groupDTO);
                }
            }
            await base.OnInitializedAsync();
        }

        public async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                if (FormState == FormState.CREATE)
                {
                    await CreateGroup();
                }
                else
                {
                    await UpdateGroup();
                }
            }
        }

        private GroupModel ProvideGroupModelValues(BasicGroupDTO groupDTO)
        {
            return new GroupModel
            {
                IdGroup = groupDTO.IdGroup,
                GroupName = groupDTO.GroupName,
                NumberOfParticipants = groupDTO.NumberOfParticipants,
                Description = groupDTO.Description,
                GroupType = groupDTO.GroupType,
                Base64dataPicture = groupDTO.Base64dataPicture,
                IdGroupOwner = groupDTO.IdGroupOwner
            };
        }

        private async Task CreateGroup()
        {
            var groupDTO = await GroupRepository.CreateGroup(Model);
            var groupAccountDTO = await GroupAccountRepository.CreateGroupAccount(new GroupAccountModel
            {
                IdAccount = groupDTO.IdGroupOwner,
                IdGroup = groupDTO.IdGroup
            });
            if (!string.IsNullOrEmpty(groupDTO.Detail) || !string.IsNullOrEmpty(groupAccountDTO.Detail))
            {
                SnackBar.Add(groupAccountDTO.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo("/groups");
                SnackBar.Add("Group creation succeed", Severity.Success);
            }
        }

        private async Task UpdateGroup()
        {
            BasicGroupDTO groupDTO = await GroupRepository.UpdateGroup(Model);
            if (!string.IsNullOrEmpty(groupDTO.Detail))
            {
                SnackBar.Add(groupDTO.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo("/groups");
                SnackBar.Add("Group update succeed", Severity.Success);
            }
        }

        private async Task ImageUpload()
        {
            var parameters = new DialogParameters();
            parameters.Add("ButtonText", "Upload");
            parameters.Add("ImageURL", Model.Base64dataPicture is not null ? Model.Base64dataPicture : null);
            parameters.Add("Color", Color.Success);

            var dialog = DialogService.Show<UploadImageDialogComponent>("Image Upload", parameters);
            var dialogResult = await dialog.Result;
            if (!dialogResult.Cancelled)
            {
                Model.Base64dataPicture = dialogResult.Data.ToString();
            }
        }
    }
}
using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.GroupAnnouncement;
using CharityApplication.Client.Validators.GroupAnnouncement;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using CharityApplication.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Shared.GroupAnnouncement
{
    public partial class GroupAnnouncementFormComponent
    {
        public GroupAnnouncementModel Model { get; set; } = new GroupAnnouncementModel();
        public BasicAccountDTO LoggedUser { get; set; } = new BasicAccountDTO();
        public GroupAnnouncementModelValidator Validator { get; set; } = null!;
        public MudForm? Form;

        [Parameter]
        public int IdGroupAnnouncement { get; set; }

        [Parameter]
        public int IdGroup { get; set; }

        [Parameter]
        public FormState FormState { get; set; }

        [Inject]
        public IGroupAnnouncementRepository GroupAnnouncementRepository { get; set; }

        private string FormTitle = string.Empty;
        private string ButtonTitle = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Validator = new GroupAnnouncementModelValidator();

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
                FormTitle = "Group Announcement Creation";
                ButtonTitle = "Create Group Announcement";
                Model.IdOwner = LoggedUser.IdAccount;
                Model.IdGroup = IdGroup;
            }
            else
            {
                FormTitle = "Group Announcement Edition";
                ButtonTitle = "Update Group Announcement";
                var groupAnnouncementDTO = await GroupAnnouncementRepository.GetGroupAnnouncement(IdGroupAnnouncement);
                if (!string.IsNullOrEmpty(groupAnnouncementDTO.Detail))
                {
                    SnackBar.Add(groupAnnouncementDTO.Detail, Severity.Error);
                }
                Model = ProvideGroupAnnouncementValues(groupAnnouncementDTO);
            }
            await base.OnInitializedAsync();
        }

        private GroupAnnouncementModel ProvideGroupAnnouncementValues(BasicGroupAnnouncementDTO groupAnnouncementDTO)
        {
            return new GroupAnnouncementModel
            {
                IdGroupAnnouncement = groupAnnouncementDTO.IdGroupAnnouncement,
                Subject = groupAnnouncementDTO.Subject,
                Message = groupAnnouncementDTO.Message,
                IdGroup = groupAnnouncementDTO.IdGroup,
                IdOwner = groupAnnouncementDTO.IdOwner,
            };
        }

        public async Task Submit()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                if (FormState == FormState.CREATE)
                {
                    await CreateGroupAnnouncement();
                }
                else
                {
                    await UpdateGroupAnnouncement();
                }
            }
        }

        private async Task CreateGroupAnnouncement()
        {
            BasicGroupAnnouncementDTO groupAnnouncementDTO = await GroupAnnouncementRepository.CreateGroupAnnouncement(Model);
            if (!string.IsNullOrEmpty(groupAnnouncementDTO.Detail))
            {
                SnackBar.Add(groupAnnouncementDTO.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo($"/group/{IdGroup}");
                SnackBar.Add("Group announcement creation succeed", Severity.Success);
            }
        }

        private async Task UpdateGroupAnnouncement()
        {
            BasicGroupAnnouncementDTO groupAnnouncementDTO = await GroupAnnouncementRepository.UpdateGroupAnnouncement(Model);
            if (!string.IsNullOrEmpty(groupAnnouncementDTO.Detail))
            {
                SnackBar.Add(groupAnnouncementDTO.Detail, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo($"/group/{IdGroup}");
                SnackBar.Add("Group announcement update succeed", Severity.Success);
            }
        }
    }
}
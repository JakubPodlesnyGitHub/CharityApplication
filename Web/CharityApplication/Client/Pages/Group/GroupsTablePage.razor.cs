using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.AccountGroup;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Pages.Group
{
    public partial class GroupsTablePage
    {
        public string SearchValue { get; set; } = "";
        public bool DenseItems { get; set; } = false;
        private bool LoadingBar = true;
        public BasicGroupDTO SelectedElement { get; set; } = null;
        public List<BasicGroupDTO> PublicGroups { get; set; } = new List<BasicGroupDTO>();
        public List<BasicGroupDTO> LoggedUserGroups = new List<BasicGroupDTO>();
        public BasicAccountDTO LoggedUser { get; set; } = new BasicAccountDTO();

        [Inject]
        public IGroupRepository GroupRepository { get; set; }

        [Inject]
        public IGroupAccountRepository GroupAccountRepository { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                LoggedUser = JsonSerializer.Deserialize<BasicAccountDTO>(
                                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
                PublicGroups = await GroupRepository.GetPublicPrivateGroups(LoggedUser.IdAccount);
                LoggedUserGroups = await GroupRepository.GetGroupsByAccountId(LoggedUser.IdAccount);
                LoadingBar = false;
            }
            else
            {
                PublicGroups = await GroupRepository.GetPublicGroups();
                LoadingBar = false;
            }
            await base.OnInitializedAsync();
        }

        private async Task NavigateToSpecificGroup(TableRowClickEventArgs<BasicGroupDTO> tableRowClickGroupArgs)
        {
            var user = (await AuthenticationStateTask).User;
            if (user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo($"/group/{tableRowClickGroupArgs.Item.IdGroup}");
            }
        }

        private async Task<BasicGroupAccountDTO> JoinToGroup(BasicGroupDTO groupDTO)
        {
            BasicGroupAccountDTO groupAccountDTO = await GroupAccountRepository.CreateGroupAccount(new GroupAccountModel
            {
                IdAccount = LoggedUser.IdAccount,
                IdGroup = groupDTO.IdGroup
            });
            if (string.IsNullOrEmpty(groupAccountDTO.Detail))
            {
                LoggedUserGroups.Add(groupDTO);
                StateHasChanged();
                SnackBar.Add("You are now a member of the group", Severity.Success);
            }
            else
            {
                SnackBar.Add(groupAccountDTO.Detail, Severity.Error);
            }
            return groupAccountDTO;
        }

        private async Task<BasicGroupAccountDTO> LeaveTheGroup(BasicGroupDTO groupDTO)
        {
            BasicGroupAccountDTO groupAccountDTO = await GroupAccountRepository.DeleteGroupAccount(LoggedUser.IdAccount, groupDTO.IdGroup);
            if (string.IsNullOrEmpty(groupAccountDTO.Detail))
            {
                LoggedUserGroups.RemoveAll(x => x.IdGroup == groupDTO.IdGroup);
                StateHasChanged();
                SnackBar.Add("You are no longer member of the group", Severity.Success);
            }
            else
            {
                SnackBar.Add(groupAccountDTO.Detail, Severity.Error);
            }
            return groupAccountDTO;
        }

        private bool FilterFuncShort(BasicGroupDTO group) => FilterFunc(group, SearchValue);

        private bool FilterFunc(BasicGroupDTO group, string SearchValue)
        {
            if (group.GroupName.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (group.Description.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(SearchValue))
            {
                return true;
            }
            if ($"{group.NumberOfParticipants}".Contains(SearchValue))
            {
                return true;
            }
            return false;
        }
    }
}
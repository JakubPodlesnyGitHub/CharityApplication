using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.AccountGroup;
using CharityApplication.Client.Shared.Dialog;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Pages.Group
{
    public partial class GroupPage
    {
        [Parameter]
        public int IdGroup { get; set; }

        [Inject]
        public IGroupRepository GroupRepository { get; set; }

        [Inject]
        public IGroupAccountRepository GroupAccountRepository { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        public BasicGroupDTO Group = new BasicGroupDTO();
        public BasicAccountDTO LoggedUser = new BasicAccountDTO();
        public List<BasicAccountDTO> GroupAccounts = new List<BasicAccountDTO>();

        protected override async Task OnInitializedAsync()
        {
            Group = await GroupRepository.GetGroup(IdGroup);

            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                LoggedUser = JsonSerializer.Deserialize<BasicAccountDTO>(
                                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });
                var groupAccountsDTO = await AccountRepository.GetAccountsByGroupId(IdGroup);
                GroupAccounts = groupAccountsDTO.FindAll(x => x.IdAccount == LoggedUser.IdAccount);
            }

            await base.OnInitializedAsync();
        }

        private IDialogReference ShowDialog()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete group? This process cannot be undone.");
            parameters.Add("ButtonText", "Group");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            return DialogService.Show<ConfirmationDialogComponent>("Delete", parameters, options);
        }

        private async Task<BasicGroupDTO> DeleteGroup()
        {
            BasicGroupDTO groupDTO = null;
            var dialog = ShowDialog();
            var dialogResult = await dialog.Result;
            if (!dialogResult.Cancelled && Group.IdGroupOwner == LoggedUser.IdAccount)
            {
                groupDTO = await GroupRepository.DeleteGroup(IdGroup);
                if (!string.IsNullOrEmpty(groupDTO.Detail))
                {
                    SnackBar.Add(groupDTO.Detail, Severity.Error);
                }
                else
                {
                    NavigationManager.NavigateTo("/groups");
                    SnackBar.Add("Group has been deleted successfully", Severity.Success);
                }
            }
            else
            {
                SnackBar.Add("You cannot delete the group because you are not the owner", Severity.Warning);
            }
            return groupDTO;
        }

        private void OpenDialogInvitationLink()
        {
            var parameters = new DialogParameters
            {
                ["Link"] = $"{NavigationManager.BaseUri}group/{IdGroup}"
            };
            DialogService.Show<InvitationLinkDialogComponent>("Group Invitation", parameters);
        }

        private async Task<BasicGroupAccountDTO> LeaveTheGroup()
        {
            BasicGroupAccountDTO groupAccountDTO = await GroupAccountRepository.DeleteGroupAccount(LoggedUser.IdAccount, Group.IdGroup);
            if (string.IsNullOrEmpty(groupAccountDTO.Detail))
            {
                StateHasChanged();
                SnackBar.Add("You are no longer member of the group", Severity.Success);
            }
            else
            {
                SnackBar.Add(groupAccountDTO.Detail, Severity.Error);
            }
            return groupAccountDTO;
        }

        private async Task<BasicGroupAccountDTO> JoinToGroup()
        {
            BasicGroupAccountDTO groupAccountDTO = await GroupAccountRepository.CreateGroupAccount(new GroupAccountModel
            {
                IdAccount = LoggedUser.IdAccount,
                IdGroup = Group.IdGroup
            });
            if (string.IsNullOrEmpty(groupAccountDTO.Detail))
            {
                SnackBar.Add("You are now a member of the group", Severity.Success);
            }
            else
            {
                SnackBar.Add(groupAccountDTO.Detail, Severity.Error);
            }
            return groupAccountDTO;
        }
    }
}
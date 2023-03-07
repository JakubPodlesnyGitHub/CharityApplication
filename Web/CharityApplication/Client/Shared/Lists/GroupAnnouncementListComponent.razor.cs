using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Shared.Dialog;
using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharityApplication.Client.Shared.Lists
{
    public partial class GroupAnnouncementListComponent
    {
        [Parameter]
        public bool IsClickable { get; set; } = false;

        [Parameter]
        public bool IsDisabled { get; set; } = false;

        [Parameter]
        public string ListTitle { get; set; } = string.Empty;

        [Parameter]
        public int GroupId { get; set; }

        [Inject]
        public IGroupAnnouncementRepository GroupAnnouncementRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        public List<BasicGroupAnnouncementDTO> GroupAnnouncements { get; set; } = new List<BasicGroupAnnouncementDTO>();

        protected override async Task OnInitializedAsync()
        {
            GroupAnnouncements = await GroupAnnouncementRepository.GetGroupAnnouncements(GroupId);
            if (GroupAnnouncements is null)
            {
                GroupAnnouncements = GroupAnnouncements.OrderByDescending(x => x.CreationDate).ToList();
            }
            await base.OnInitializedAsync();
        }

        private async Task<BasicGroupAnnouncementDTO> Delete(int idGroupAnnouncement)
        {
            BasicGroupAnnouncementDTO groupAnnouncementDTO = null;
            var dialog = ShowDialog();
            var dialogResult = await dialog.Result;
            if (!dialogResult.Cancelled)
            {
                groupAnnouncementDTO = await GroupAnnouncementRepository.DeleteGroupAnnouncement(idGroupAnnouncement);
                if (string.IsNullOrEmpty(groupAnnouncementDTO.Detail))
                {
                    GroupAnnouncements.RemoveAll(x => x.IdGroupAnnouncement == groupAnnouncementDTO.IdGroupAnnouncement);
                    StateHasChanged();
                    SnackBar.Add("Group announcement has been deleted successfully", Severity.Success);
                }
                else
                {
                    SnackBar.Add(groupAnnouncementDTO.Detail, Severity.Error);
                }
            }
            return groupAnnouncementDTO;
        }

        private IDialogReference ShowDialog()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete group announcement? This process cannot be undone.");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            return DialogService.Show<ConfirmationDialogComponent>("Delete", parameters, options);
        }
    }
}
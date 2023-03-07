using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Model.AccountEvent;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Shared.Modules
{
    public partial class AttendanceListComponent
    {
        public List<BasicAccountDTO> Accounts { get; set; } = new List<BasicAccountDTO>();
        public MudTable<BasicAccountDTO>? Table { get; set; }
        public BasicAccountDTO LoggedAccount { get; set; } = new BasicAccountDTO();

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        [Inject]
        public IEventAccountRepository EventAccountRepository { get; set; }

        [Parameter]
        public int IdEvent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (await LocalStorageService.ContainKeyAsync("loggedUser"))
            {
                LoggedAccount = JsonSerializer.Deserialize<BasicAccountDTO>(
                    await LocalStorageService.GetItemAsync<string>("loggedUser"),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            Accounts = await AccountRepository.GetUnconfirmedAccountsByEventId(IdEvent);
            Accounts = Accounts.FindAll(x => x.IdAccount != LoggedAccount.IdAccount);
            await base.OnInitializedAsync();
        }

        public async Task OnRowClick(TableRowClickEventArgs<BasicAccountDTO> e)
        {
            BasicEventAccountDTO eventAccountDTO = await EventAccountRepository.UpdateEventAccount(new EventAccountModel
            {
                IdEvent = IdEvent,
                IdAccount = e.Item.IdAccount,
                IfPartcipantPresent = true
            });
            if (string.IsNullOrEmpty(eventAccountDTO.Detail))
            {
                SnackBar.Add("The presence of the user has been confirmed", Severity.Success);
                Accounts.RemoveAll(x => x.IdAccount == e.Item.IdAccount);
                StateHasChanged();
            }
            else
            {
                SnackBar.Add(eventAccountDTO.Detail, Severity.Error);
            }
        }
    }
}
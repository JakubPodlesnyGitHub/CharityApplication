using Application.Dtos.BasicDtos.Responses;
using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Shared.Model.Chat;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System.Text.Json;

namespace CharityApplication.Client.Pages.Chat
{
    public partial class ChatPage : IAsyncDisposable
    {
        public List<BasicAccountDTO> Accounts = new List<BasicAccountDTO>();
        public BasicAccountDTO? ReceiverAccount { get; set; } = null;
        public BasicAccountDTO LoggedUser { get; set; } = new BasicAccountDTO();

        [Parameter]
        public int IdReceiver { get; set; }

        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        private bool IsOpen = false;

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
            }

            Accounts = await GetContacts();
            if (IdReceiver > 0)
            {
                ReceiverAccount = await AccountRepository.GetAccount(IdReceiver);
                if (!string.IsNullOrEmpty(ReceiverAccount.Detail))
                {
                    SnackBar.Add(ReceiverAccount.Detail, Severity.Warning);
                }
                await CreateHub();
            }

            await base.OnInitializedAsync();
        }

        private async Task<List<BasicAccountDTO>> GetContacts()
        {
            var accountDTOs = await AccountRepository.GetAccounts();
            return accountDTOs.FindAll(x => x.IdAccount != LoggedUser.IdAccount);
        }

        private async Task CreateHub()
        {
            if (hubConnection is null)
            {
                hubConnection = new HubConnectionBuilder()
                                .WithUrl(NavigationManager.ToAbsoluteUri("chathub"))
                                .WithAutomaticReconnect()
                                .Build();
            }

            hubConnection.On<string, ChatMessageModel>("ReceiveMessage", (user, message) =>
                        {
                            ChatMessages.Add(message);
                            StateHasChanged();
                        });
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("Connect", new ChatConnectionModel
            {
                IdUser = LoggedUser.IdAccount.ToString(),
                ConnectionId = hubConnection.ConnectionId
            });
        }

        private HubConnection hubConnection = null!;
        public string Message { get; set; } = string.Empty;
        public List<ChatMessageModel> ChatMessages { get; set; } = new List<ChatMessageModel>();

        private async Task SendMessage()
        {
            await hubConnection.SendAsync("SendMessage", LoggedUser.IdAccount.ToString(), new ChatMessageModel
            {
                MessageText = Message,
                Sender = LoggedUser,
                Recipient = ReceiverAccount,
                IdRecipient = IdReceiver,
                IdSender = LoggedUser.IdAccount
            }, IdReceiver.ToString());
            Message = string.Empty;
        }

        private async Task LoadChat(int accountId)
        {
            NavigationManager.NavigateTo($"/chat/{accountId}");
            await OnInitializedAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                if (hubConnection.State == HubConnectionState.Connected)
                {
                    await hubConnection.InvokeAsync("Disconnect", LoggedUser.IdAccount.ToString());
                    await hubConnection.StopAsync();
                    await hubConnection.DisposeAsync();
                }
            }
        }
    }
}
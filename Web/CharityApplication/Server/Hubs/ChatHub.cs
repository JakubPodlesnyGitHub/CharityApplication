using CharityApplication.Shared.Model.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace CharityApplication.Server.Hubs
{
    public class ChatHub : Hub
    {
        private static ConcurrentDictionary<string, string> ConnectionsDictionary = new ConcurrentDictionary<string, string>();

        public async Task Connect(ChatConnectionModel data)
        {
            var userId = data.IdUser;
            var connectionId = Context.ConnectionId;
            if (!ConnectionsDictionary.ContainsKey(userId))
            {
                ConnectionsDictionary.TryAdd(userId, connectionId);
            }
            else
            {
                ConnectionsDictionary.TryUpdate(userId, connectionId, ConnectionsDictionary[userId]);
            }
        }

        public async Task SendMessage(string user, ChatMessageModel message, string to)
        {
            if (ConnectionsDictionary.ContainsKey(user) && !ConnectionsDictionary.ContainsKey(to))
            {
                await Clients.Client(ConnectionsDictionary[user]).SendAsync("ReceiveMessage", user, new ChatMessageModel { MessageText = "This person is currently unavailable" });
            }
            else
            {
                ReadOnlyCollection<string> ReadOnlyConnectionIds = new List<string>() { ConnectionsDictionary[user], ConnectionsDictionary[to] }.AsReadOnly();
                await Clients.Clients(ReadOnlyConnectionIds).SendAsync("ReceiveMessage", user, message);
            }
        }

        public void Disconnect(string userId)
        {
            if (ConnectionsDictionary.TryRemove(userId, out var removedValue))
            {
                Console.WriteLine($"User with id {userId} has left the chat");
            }
        }
    }
}
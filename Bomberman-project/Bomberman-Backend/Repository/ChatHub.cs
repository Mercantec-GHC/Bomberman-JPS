using Bomberman_Backend.Repository.Interfaces;
using Microsoft.AspNetCore.SignalR;
using DomainModels;

namespace Bomberman_Backend.Repository
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(SignalRSendType input)
        {
            await Clients.All.SendAsync("ReceiveMessage", input.Type, input.User, input.Message);
            Console.WriteLine($"Type: {input.Type}, {input.User}: {input.Message}");
        }
        public async Task SendPrivateMessage(string user, string message)
        {
            var connectionId = Context.ConnectionId;
            await Clients.User(user).SendAsync("ReceivePrivateMessage", connectionId, message);
        }
    }
}

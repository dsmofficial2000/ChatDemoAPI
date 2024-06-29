using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatDemoAPI2.Hubs
{
    public class ChatHub : Hub
    {
        [Authorize]
        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageToUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }

        public override Task OnConnectedAsync()
        {
            string username = Context.User.Identity.Name;
            UserHandler.ConnectedIds[username] = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string username = Context.User.Identity.Name;
            UserHandler.ConnectedIds.Remove(username);
            return base.OnDisconnectedAsync(exception);
        }
    }

    public static class UserHandler
    {
        public static Dictionary<string, string> ConnectedIds = new Dictionary<string, string>();
    }
}

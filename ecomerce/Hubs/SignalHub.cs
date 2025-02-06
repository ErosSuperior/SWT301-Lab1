using Microsoft.AspNetCore.SignalR;

namespace ecomerce.Hubs
{
    public class SignalHub:Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Implement your logic here to handle incoming messages
            // and broadcast them to connected clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

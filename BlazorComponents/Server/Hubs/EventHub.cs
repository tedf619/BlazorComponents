using Microsoft.AspNetCore.SignalR;

namespace BlazorComponents.Server
{
    public class EventHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await ModelessDialogMessage("dialog closed");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task ModelessDialogMessage(string message)
        {
            await Clients.All.SendAsync("ModelessDialogMessage", message);
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorComponents.Client.Shared
{
    /// <summary>
    /// This class uses SignalR to send events via the server back to the popup window's parent
    /// </summary>
    public class PopupWindowBase : ComponentBase, IAsyncDisposable
    {
        HubConnection? hubConnection;

        [Inject]
        public NavigationManager? navigationManager { get; set; }

        public virtual async ValueTask DisposeAsync()
        {
            if (hubConnection == null) return;
            await hubConnection.DisposeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await ConnectHub();
        }

        protected async Task Send(string text)
        {
            if (hubConnection == null) return;
            await hubConnection.SendAsync("ModelessDialogMessage", text);
        }

        protected async Task ConnectHub()  // connects to server via SignalR hub
        {
            Uri uri = navigationManager == null ? new Uri("") : navigationManager.ToAbsoluteUri("/eventhub");

            hubConnection = new HubConnectionBuilder()
                .WithUrl(uri)
                .Build();

            hubConnection.On<string>("ModelessDialogMessage", (message) => HandleEvent(message));

            await hubConnection.StartAsync();
        }

        protected virtual void HandleEvent(string message)
        {
            // handle events in derived class
        }
    }
}

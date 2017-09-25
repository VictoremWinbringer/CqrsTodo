using Microsoft.AspNetCore.SignalR;

namespace CqrsTodo.SignalR
{
    public class Notifier : Hub
    {
        public void Notify(string message)
        {
            Clients.All.InvokeAsync("Notify", message);
        }
    }
}

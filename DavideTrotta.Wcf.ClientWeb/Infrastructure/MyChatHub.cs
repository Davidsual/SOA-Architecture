using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace DavideTrotta.Wcf.ClientWeb.Infrastructure
{
    [HubName("MyTesttHub")]
    public class MyTesttHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello("Dave");
        }

        public void BroadcastMessageToAll(string message)
        {
            Clients.All.newMessageReceived(message);
        }
        public void JoinAGroup(string group)
        {
            Groups.Add(Context.ConnectionId, group);
        }

        public void RemoveFromAGroup(string group)
        {
            Groups.Remove(Context.ConnectionId, group);
        }

        public void BroadcastToGroup(string message, string group)
        {
            Clients.Group(group).newMessageReceived(message);
        }
    }
}


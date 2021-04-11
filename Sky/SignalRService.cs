using Microsoft.AspNetCore.SignalR.Client;
using Sky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky
{
    public class SignalRService
    {
        private HubConnection hub = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/Chat")
            .Build();
        public event Action<Message> MessageReceived;
        public async Task StartConnection()
        {
            await hub.StartAsync();
            hub.On<Message>("ReceiveMessage", (message) => MessageReceived.Invoke(message));
        }
        public async Task addToGroup(string chatId)
        {
            await hub.InvokeAsync<string>("JoinChatGroup", chatId);
        }
        public async Task SendMessage(Message message)
        {
            await hub.InvokeAsync<Message>("SendMessages", message);
        }
    }
}

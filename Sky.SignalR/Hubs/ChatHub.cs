using Microsoft.AspNetCore.SignalR;
using Sky.Models;
using System;
using System.Threading.Tasks;

namespace Sky.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessages(Message message)
        {
            await Clients.Group(message.Chat_id.ToString()).SendAsync("ReceiveMessage", message);
            Console.WriteLine("Сообщение успешно отправлено!");
        }
        public async void JoinChatGroup(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            Console.WriteLine("Успешно добавлен в группу!");
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacToe.Data.Enums;

namespace TicTacToe
{
    public class MultiPlayerHub : Hub
    {
        public const string HubUrl = "/multi-player";

        public async Task Broadcast(BoardCell cell, int index)
        {
            await Clients.All.SendAsync("Broadcast", cell, index);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}

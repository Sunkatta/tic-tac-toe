using System;
using System.Collections.Generic;
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
            if (UserHandler.ConnectedIds.Count < 2)
            {
                UserHandler.ConnectedIds.Add(Context.ConnectionId);
                return base.OnConnectedAsync();
            }

            //Clients.Client(Context.ConnectionId).SendAsync("DisconnectAsync");
            //return Task.CompletedTask;
            // throw new Exception("FULL");

            return OnDisconnectedAsync(new Exception("FULL"));
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(e);
        }

        private static class UserHandler
        {
            public static HashSet<string> ConnectedIds = new HashSet<string>();
        }
    }
}

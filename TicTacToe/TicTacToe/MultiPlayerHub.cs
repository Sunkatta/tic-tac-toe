using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacToe.Data.Enums;
using TicTacToe.Messages;

namespace TicTacToe
{
    public class MultiPlayerHub : Hub
    {
        public const string HubUrl = "/multi-player";

        public async Task Broadcast(Message message)
        {
            await Clients.All.SendAsync("Broadcast", message);
        }

        public override Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(e);
        }

        public BoardCell GetPlayerCell()
        {
            return UserHandler.ConnectedIds.Count switch
            {
                1 => BoardCell.X,
                2 => BoardCell.O,
                _ => BoardCell.EMPTY,
            };
        }

        public bool CanStartGame()
        {
            return UserHandler.ConnectedIds.Count == 2;
        }

        private static class UserHandler
        {
            public static HashSet<string> ConnectedIds = new HashSet<string>();
        }
    }
}

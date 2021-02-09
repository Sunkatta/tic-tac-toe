using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Components
{
    public class MultiPlayerComponent : BasePlayerComponent
    {
        public override void PlayAgain()
        {
            throw new NotImplementedException();
        }

        // name of the user who will be chatting
        private BoardCell playerCell;

        // on-screen message
        private int index;

        // new message input
        private string newMessage;

        private string hubUrl;
        private HubConnection hubConnection;

        public string BaseUri { get; set; }

        public async Task ConnectPlayer()
        {
            try
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubUrl)
                    .Build();

                hubConnection.On<BoardCell, int>("Broadcast", BroadcastPlayerMove);

                await hubConnection.StartAsync();

                //await SendAsync($"[Notice] {username} joined chat room.");
            }
            catch (Exception e)
            {
                //message = $"ERROR: Failed to start chat client: {e.Message}";
            }
        }

        private void BroadcastPlayerMove(BoardCell cell, int index)
        {
            //bool isMine = name.Equals(username, StringComparison.OrdinalIgnoreCase);

            // Inform blazor the UI needs updating
            StateHasChanged();
        }

        private async Task DisconnectAsync()
        {
            //await SendAsync($"[Notice] {username} left chat room.");

            await hubConnection.StopAsync();
            await hubConnection.DisposeAsync();

            hubConnection = null;
        }

        public override async void HandleTileClick()
        {
            //TODO: handle send move to the other scrub
            await hubConnection.SendAsync("Broadcast", playerCell, index);
            base.HandleTileClick();
        }
    }
}

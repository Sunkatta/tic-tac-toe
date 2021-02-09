using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using TicTacToe.Data.Enums;

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
        public int index;

        // new message input
        private string newMessage;

        public string hubUrl;

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
                hubConnection.On("OnDisconnectedAsync", DisconnectAsync);

                await hubConnection.StartAsync();
            }
            catch (Exception e)
            {
                await DisconnectAsync();
                string message = $"ERROR: Failed to start chat client: {e.Message}";
            }
        }

        private void BroadcastPlayerMove(BoardCell cell, int index)
        {
            //bool isMine = name.Equals(username, StringComparison.OrdinalIgnoreCase);

            GameManager.ExecuteMove(index);
            base.HandleTileClick(index);

            StateHasChanged();
        }

        private async Task DisconnectAsync()
        {
            //await SendAsync($"[Notice] {username} left chat room.");

            await hubConnection.StopAsync();
            await hubConnection.DisposeAsync();

            hubConnection = null;
        }

        public override async void HandleTileClick(int index)
        {
            await hubConnection.SendAsync("Broadcast", playerCell, index);
        }
    }
}

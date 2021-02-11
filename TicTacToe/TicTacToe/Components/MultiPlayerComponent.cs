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
        public BoardCell PlayerCell { get; set; }

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
                await hubConnection.StartAsync();
                PlayerCell = await hubConnection.InvokeAsync<BoardCell>("GetPlayerCell");

                if (PlayerCell == BoardCell.EMPTY)
                {
                    await DisconnectAsync();
                }
            }
            catch (Exception e)
            {
                await DisconnectAsync();
                string message = $"ERROR: Failed to start chat client: {e.Message}";
            }
        }

        private void BroadcastPlayerMove(BoardCell cell, int index)
        {
            GameManager.ExecuteMove(cell, index);
            HandleGameFinish(index);
            StateHasChanged();
        }

        private async Task DisconnectAsync()
        {
            await hubConnection.StopAsync();
            await hubConnection.DisposeAsync();

            hubConnection = null;
        }

        public async void HandleTileClick(int index)
        {
            if (hubConnection != null && GameManager.PlayerTurn == PlayerCell)
            {
                await hubConnection.SendAsync("Broadcast", PlayerCell, index);
            }
        }
    }
}

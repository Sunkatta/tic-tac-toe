using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using TicTacToe.Data.Enums;
using TicTacToe.Messages;

namespace TicTacToe.Components
{
    public class MultiPlayerComponent : BasePlayerComponent
    {
        public override void PlayAgain()
        {
            throw new NotImplementedException();
        }

        public BoardCell PlayerCell { get; set; }

        public string hubUrl;

        private HubConnection hubConnection;

        public string BaseUri { get; set; }
        public bool CanStartGame { get; set; }

        public async Task ConnectPlayer()
        {
            try
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubUrl)
                    .Build();

                hubConnection.On<Message>("Broadcast", BroadcastMessage);
                await hubConnection.StartAsync();
                PlayerCell = await hubConnection.InvokeAsync<BoardCell>("GetPlayerCell");
                CanStartGame = await hubConnection.InvokeAsync<bool>("CanStartGame");

                if (PlayerCell == BoardCell.EMPTY)
                {
                    await DisconnectAsync();
                }

                if (CanStartGame)
                {
                    await hubConnection.SendAsync("Broadcast", new Message(MessageType.START_GAME, true));
                }
            }
            catch (Exception e)
            {
                await DisconnectAsync();
                string message = $"ERROR: Failed to start chat client: {e.Message}";
            }
        }

        //TODO: Needs Refactoring Consider using Visitor Pattern
        private void BroadcastMessage(Message message)
        {
            JsonElement jsonElement = (JsonElement)message.Content;
            switch (message.Type)
            {
                case MessageType.START_GAME:
                    CanStartGame = jsonElement.GetBoolean();
                    break;
                case MessageType.PLAYER_MOVE:
                    BoardCell playerCell = (BoardCell)Enum.Parse(typeof(BoardCell), jsonElement.GetProperty("playerCell").ToString());
                    int index = jsonElement.GetProperty("index").GetInt32();
                    GameManager.ExecuteMove(playerCell, index);
                    HandleGameFinish(index);
                    break;
                case MessageType.RESTART_GAME:
                    //TBD
                    break;
                default:
                    break;
            }

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
            if (hubConnection != null)
            {
                PlayerMove playerMove = new PlayerMove(PlayerCell, index);
                await hubConnection.SendAsync("Broadcast", new Message(MessageType.PLAYER_MOVE, playerMove));
            }
        }
    }
}

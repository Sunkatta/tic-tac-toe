using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game;
using TicTacToe.Data.Game.Managers;
using TicTacToe.Messages;

namespace TicTacToe.Components
{
    public class MultiPlayerComponent : BasePlayerComponent, IDisposable
    {
        public override async void PlayAgain()
        {
            contextBuilder = new GameContext.Builder(GameMode.MP);
            GameManager = new GameManager(contextBuilder.Build());
            IsLoaded = false;
            CanStartGame = false;
            IsInterrupted = false;
            PlayerCell = BoardCell.EMPTY;
            StateHasChanged();
            await ConnectPlayer();
        }

        public BoardCell PlayerCell { get; private set; }

        public string hubUrl;

        private HubConnection hubConnection;

        public string BaseUri { get; set; }

        public bool CanStartGame { get; private set; }

        public bool IsLoaded { get; private set; }

        public bool IsInterrupted { get; private set; }

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

                IsLoaded = true;
                StateHasChanged();
            }
            catch (Exception e)
            {
                await DisconnectAsync();
                string message = $"ERROR: Failed to start chat client: {e.Message}";
            }
        }

        //TODO: Needs Refactoring Consider using Visitor Pattern
        private async void BroadcastMessage(Message message)
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
                case MessageType.DISCONNECT_GAME:
                    IsInterrupted = jsonElement.GetBoolean();
                    await DisconnectAsync();
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

        public override async void HandleGameFinish(int index)
        {
            base.HandleGameFinish(index);

            if (GameManager.IsGameFinished)
            {
                await DisconnectAsync();
            }
        }

        public async void Dispose()
        {
            if (hubConnection != null)
            {
                await hubConnection.SendAsync("Broadcast", new Message(MessageType.DISCONNECT_GAME, true));
            }
        }
    }
}

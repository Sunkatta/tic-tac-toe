using Microsoft.AspNetCore.Components;
using System.Text;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Components
{
    public class BoardComponent : ComponentBase
    {
        [Parameter]
        public string Local { get; set; }

        public IGameManager GameManager { get; set; }

        public BoardCell[] Tiles { get; set; }

        public string EndGameTitle { get; set; }
        public string EndGameMessage { get; set; }

        protected override void OnInitialized()
        {
            GameMode mode = string.IsNullOrEmpty(Local) ? GameMode.SP_AI : GameMode.SP_LOCAL;
            GameContext.Builder contextBuilder = new GameContext.Builder(mode);
            if (mode == GameMode.SP_AI)
            {
                //for testing purposes
                contextBuilder.WithDifficulty(AIDifficulty.EASY);
            }

            GameManager = new GameManager(contextBuilder.Build());
            Tiles = GameManager.NewGame();

        }

        protected void ClickTile(int index)
        {
            if (!GameManager.IsGameFinished)
            {
                GameManager.ExecuteMove(index);
            }
            
            if (GameManager.IsGameFinished)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GameManager.Winner == BoardCell.EMPTY ? "No one won this round!" : $"Player {GameManager.Winner} Wins!");
                sb.Append("<br>");
                sb.Append("Do you want to play again?");
                EndGameMessage = sb.ToString();
                EndGameTitle = "The Game Has Finished!";
            }
        }


        public void PlayAgain()
        {
            Tiles = GameManager.NewGame();
            StateHasChanged();
        }
    }
}

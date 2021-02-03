using Microsoft.AspNetCore.Components;
using System.Text;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Components
{
    public class SinglePlayerComponent : BasePlayerComponent
    {
        [Parameter]
        public string Local { get; set; }

        public bool isDificultyPicked = false;

        protected override void OnInitialized()
        {
            mode = string.IsNullOrEmpty(Local) ? GameMode.SP_AI : GameMode.SP_LOCAL;

            contextBuilder = new GameContext.Builder(mode);
            if (mode == GameMode.SP_LOCAL)
            {
                InitializeGameManager();
            }
        }

        protected void ChooseDifficulty(AIDifficulty difficulty)
        {
            if (contextBuilder != null)
            {
                contextBuilder.WithDifficulty(difficulty);
                InitializeGameManager();
            }
        }

        public override void PlayAgain()
        {
            GameManager.NewGame();
            StateHasChanged();
        }

        public void HandleTileClick()
        {
            if (GameManager.IsGameFinished)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GameManager.Winner == BoardCell.EMPTY ? "No one won this round!" : $"Player {GameManager.Winner} Wins!");
                sb.Append("<br>");
                sb.Append("Do you want to play again?");
                EndGameMessage = sb.ToString();
                EndGameTitle = "The Game Has Finished!";
                StateHasChanged();
            }
        }

        private void InitializeGameManager()
        {
            GameManager = new GameManager(contextBuilder.Build());
            isDificultyPicked = true;
        }
    }
}

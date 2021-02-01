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

        public AIDifficulty Difficulty { get; set; }

        public string EndGameTitle { get; set; }

        public string EndGameMessage { get; set; }

        public bool isDificultyPicked = false;

        public GameMode mode;

        protected override void OnInitialized()
        {
            mode = string.IsNullOrEmpty(Local) ? GameMode.SP_AI : GameMode.SP_LOCAL;

            if (mode == GameMode.SP_LOCAL)
            {
                GameContext.Builder contextBuilder = new GameContext.Builder(mode);
                GameManager = new GameManager(contextBuilder.Build());
                Tiles = GameManager.NewGame();
                isDificultyPicked = true;
            }
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

        protected void ChooseDifficulty(AIDifficulty difficulty)
        {
            GameContext.Builder contextBuilder = new GameContext.Builder(mode);
            contextBuilder.WithDifficulty(difficulty);
            GameManager = new GameManager(contextBuilder.Build());
            Tiles = GameManager.NewGame();
            isDificultyPicked = true;
        }

        protected void PlayAgain()
        {
            Tiles = GameManager.NewGame();
            StateHasChanged();
        }
    }
}

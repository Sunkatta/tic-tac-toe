using Microsoft.AspNetCore.Components;
using System.Text;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Components
{
    public abstract class BasePlayerComponent : ComponentBase
    {
        protected IGameManager GameManager { get; set; }

        protected string EndGameTitle { get; set; }

        protected string EndGameMessage { get; set; }

        protected GameMode mode;

        protected GameContext.Builder contextBuilder;

        public abstract void PlayAgain();

        // TODO: Rename and remove virtual
        public virtual void HandleTileClick(int index)
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
    }
}

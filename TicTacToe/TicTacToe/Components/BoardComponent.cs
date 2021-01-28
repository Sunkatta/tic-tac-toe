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

        public GameContext Context { get; set; }
        public IBoardManager BoardManager { get; set; }

        public BoardCell[] Tiles { get; set; }
        public BoardCell playerTurn = BoardCell.X;
        public bool isFinished = false;

        public string EndGameTitle { get; set; }
        public string EndGameMessage { get; set; }

        protected override void OnInitialized()
        {
            GameMode mode = string.IsNullOrEmpty(Local) ? GameMode.SP_AI : GameMode.SP_LOCAL;
            GameContext.Builder builder = new GameContext.Builder(mode);
            if (mode == GameMode.SP_AI)
            {
                builder.WithDifficulty(AIDifficulty.EASY);
            }

            Context = builder.Build();

            BoardManager = new BoardManager();
            Tiles = BoardManager.NewGame();

        }

        protected void ClickTile(int index)
        {
            if (!isFinished && BoardManager.ValidMove(index))
            {
                bool isX = playerTurn == BoardCell.X;
                BoardManager.PerformMove(isX ? BoardCell.X : BoardCell.O, index);
                playerTurn = isX ? BoardCell.O : BoardCell.X;
                isFinished = BoardManager.IsFinished();
            }
            
            if (isFinished)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(BoardManager.Winner == BoardCell.EMPTY ? "No one won this round!" : $"Player {BoardManager.Winner} Wins!");
                sb.Append("<br>");
                sb.Append("Do you want to play again?");
                EndGameMessage = sb.ToString();
                EndGameTitle = "The Game Has Finished!";
            }
        }


        public void PlayAgain()
        {
            Tiles = BoardManager.NewGame();
            isFinished = false;
            StateHasChanged();
        }
    }
}

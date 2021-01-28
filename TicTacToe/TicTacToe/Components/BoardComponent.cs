using Microsoft.AspNetCore.Components;
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
        private bool isFinished = false;

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
            if (!isFinished)
            {
                bool isX = playerTurn == BoardCell.X;
                BoardManager.PerformMove(isX ? BoardCell.X : BoardCell.O, index);
                playerTurn = isX ? BoardCell.O : BoardCell.X;
                isFinished = BoardManager.HasWinner();
            }
        }


        public void PlayAgain()
        {
            Tiles = BoardManager.NewGame();
        }
    }
}

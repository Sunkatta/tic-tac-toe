using Microsoft.AspNetCore.Components;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game;

namespace TicTacToe.Components
{
    public class BoardComponent : ComponentBase
    {
        [Parameter]
        public string Local { get; set; }

        public GameContext Context { get; set; }
        public IGameManager GameManager { get; set; }

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

            GameManager = new GameManager(Context);
            Tiles = GameManager.NewGame();

        }

        protected void ClickTile(int index)
        {
            if (Tiles[index] == BoardCell.EMPTY && !isFinished)
            {
                bool isX = playerTurn == BoardCell.X;
                Tiles[index] = isX ? BoardCell.X : BoardCell.O;

                isFinished = GameManager.IsWinner(playerTurn);
                playerTurn = isX ? BoardCell.O : BoardCell.X;
            }
        }


        public void PlayAgain()
        {
            Tiles = GameManager.NewGame();
        }
    }
}

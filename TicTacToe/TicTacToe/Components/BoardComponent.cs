using Microsoft.AspNetCore.Components;
using TicTacToe.Data.Enums;

namespace TicTacToe.Components
{
    public class BoardComponent : ComponentBase
    {
        [Parameter]
        public string Local { get; set; }

        public GameMode GameMode { get; set; }

        public BoardCell[] tiles = new BoardCell[9];
        public BoardCell playerTurn = BoardCell.X;

        protected override void OnInitialized()
        {
            GameMode = string.IsNullOrEmpty(Local) ? GameMode.SP_AI : GameMode.SP_LOCAL;
        }

        protected void ClickTile(int index)
        {
            if (tiles[index] == BoardCell.EMPTY)
            {
                bool isX = playerTurn == BoardCell.X;
                tiles[index] = isX ? BoardCell.X : BoardCell.O;
                playerTurn = isX ? BoardCell.O : BoardCell.X;
            }
        }
    }
}

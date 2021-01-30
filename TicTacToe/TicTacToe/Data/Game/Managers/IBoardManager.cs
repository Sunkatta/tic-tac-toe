using System.Collections.Generic;
using TicTacToe.Data.Enums;

namespace TicTacToe.Data.Game.Managers
{
    public interface IBoardManager
    {
        bool HasWinner();

        bool IsFinished();

        BoardCell Winner { get; }


        IList<int> GetEmptyBoardCellsIndexes();


        BoardCell[] GetAllBoardCells();

        BoardCell[] NewBoard();

        bool IsWinner(BoardCell cell);

        bool PerformMove(BoardCell playerCell, int index);
    }
}

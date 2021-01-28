using TicTacToe.Data.Enums;

namespace TicTacToe.Data.Game.Managers
{
    public interface IBoardManager
    {
        bool HasWinner();

        bool IsFinished();

        BoardCell Winner { get; }


        BoardCell[] GetAvailableBoardCells();


        BoardCell[] GetAllBoardCells();

        BoardCell[] NewGame();

        bool IsWinner(BoardCell cell);

        void PerformMove(BoardCell playerCell, int index);

    }
}

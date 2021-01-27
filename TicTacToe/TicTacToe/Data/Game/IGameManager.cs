using TicTacToe.Data.Enums;

namespace TicTacToe.Data.Game
{
    public interface IGameManager
    {
        bool HasWinner();

        bool IsFinished();

        BoardCell Winner { get; }


        BoardCell[] GetAvailableBoardCells();


        BoardCell[] GetAllBoardCells();

        BoardCell[] NewGame();

        bool IsWinner(BoardCell cell);


    }
}

using TicTacToe.Data.Enums;

namespace TicTacToe.Data.Game.Managers
{
    public interface IGameManager
    {
        void ExecuteMove(int index);

        void ExecuteMove(BoardCell cell, int index);

        BoardCell PlayerTurn { get; }

        BoardCell Winner { get; }

        BoardCell[] NewGame();

        bool IsGameFinished { get; }

        IBoardManager BoardManager { get; }

        GameContext Context { get; }

    }
}

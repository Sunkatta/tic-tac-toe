using TicTacToe.Data.Enums;

namespace TicTacToe.Data.Game.Players
{
    public interface IPlayer
    {
        bool PerformMove(int index);

        BoardCell PlayerCell { get; }

    }
}

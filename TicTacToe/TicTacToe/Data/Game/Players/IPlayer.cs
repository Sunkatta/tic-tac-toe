using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Data.Game.Players
{
    public interface IPlayer
    {
        void PerformMove(int index);

        BoardCell PlayerCell { get; set; }

        BoardManager BoardManager{ get; set; }

    }
}

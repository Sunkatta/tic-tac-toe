using TicTacToe.Data.Enums;

namespace TicTacToe.Messages
{
    public class PlayerMove
    {
        public BoardCell PlayerCell { get; private set; }
        public int Index { get; private set; }

        public PlayerMove(BoardCell playerCell, int index)
        {
            PlayerCell = playerCell;
            Index = index;
        }
    }
}

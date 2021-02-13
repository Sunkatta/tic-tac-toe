using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Data.Game.Players.AIStrategies
{
    public class MediumAIStrategy : BaseAIStrategy
    {
        private readonly int[] preferredMoves = { 4, 0, 2, 6, 8, 1, 3, 5, 7 };

        public MediumAIStrategy(BoardCell aiCell, IBoardManager boardManager) : base(aiCell, boardManager)
        {
            // do nothing
        }

        public override int GenerateMove()
        {
            foreach (int index in preferredMoves)
            {
                if (BoardManager.GetAllBoardCells()[index] == BoardCell.EMPTY)
                {
                    return index;
                }
            }
            return -1;
        }
    }
}

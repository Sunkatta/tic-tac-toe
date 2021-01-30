using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Data.Game.Players.AIStrategies
{
    public abstract class BaseAIStrategy : IAIStrategy
    {
        protected IBoardManager BoardManager { get; private set; }

        public BoardCell AICell { get; private set; }
        public BaseAIStrategy(BoardCell aiCell, IBoardManager boardManager)
        {
            AICell = aiCell;
            BoardManager = boardManager;
        }

        public abstract int GenerateMove();
    }
}

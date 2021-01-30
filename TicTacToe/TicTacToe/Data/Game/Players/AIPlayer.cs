using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;
using TicTacToe.Data.Game.Players.AIStrategies;

namespace TicTacToe.Data.Game.Players
{
    public class AIPlayer : Player
    {
        private IAIStrategy aiStrategy;
        public AIPlayer(BoardCell aiCell, IBoardManager boardManager, AIDifficulty difficulty) : base(aiCell, boardManager)
        {
            aiStrategy = AIStrategyProvider.Provide(aiCell, boardManager, difficulty);
        }

        public bool PerformMove()
        {
            int index = aiStrategy.GenerateMove();
            return PerformMove(index);
        }
    }
}

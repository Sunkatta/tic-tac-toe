using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Data.Game.Players.AIStrategies
{
    public class AIStrategyProvider
    {
        public static IAIStrategy Provide(BoardCell aiCell, IBoardManager boardManager, AIDifficulty difficulty)
        {
            return difficulty switch
            {
                AIDifficulty.EASY => new EasyAIStrategy(aiCell, boardManager),
                AIDifficulty.MEDIUM => new MediumAIStrategy(aiCell, boardManager),
                AIDifficulty.HARD => new HardAIStrategy(aiCell, boardManager),
                _ => throw new System.NotSupportedException(),
            };
        }
    }
}

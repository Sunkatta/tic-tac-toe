using System;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Data.Game.Players.AIStrategies
{
    public class HardAIStrategy : BaseAIStrategy
    {
        public HardAIStrategy(BoardCell aiCell, IBoardManager boardManager) : base(aiCell, boardManager)
        {
        }

        public override int GenerateMove()
        {
            throw new NotImplementedException();
        }
    }
}

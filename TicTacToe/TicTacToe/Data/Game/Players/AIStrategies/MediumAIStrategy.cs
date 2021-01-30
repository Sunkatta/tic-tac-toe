using System;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Data.Game.Players.AIStrategies
{
    public class MediumAIStrategy : BaseAIStrategy
    {
        public MediumAIStrategy(BoardCell aiCell, IBoardManager boardManager) : base(aiCell, boardManager)
        {
        }

        public override int GenerateMove()
        {
            throw new NotImplementedException();
        }
    }
}

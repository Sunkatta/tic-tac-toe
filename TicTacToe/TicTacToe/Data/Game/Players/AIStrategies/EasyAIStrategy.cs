using System;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Data.Game.Players.AIStrategies
{
    public class EasyAIStrategy : BaseAIStrategy
    {
        public EasyAIStrategy(BoardCell aiCell, IBoardManager boardManager) : base(aiCell, boardManager)
        {
        }

        public override int GenerateMove()
        {
            return BoardManager.GetEmptyBoardCellsIndexes().Count > 0 ? BoardManager.GetEmptyBoardCellsIndexes()[0] : -1;
        }
    }
}

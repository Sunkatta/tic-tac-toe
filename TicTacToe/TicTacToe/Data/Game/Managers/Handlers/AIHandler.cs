using System;
using System.Collections.Generic;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Players;

namespace TicTacToe.Data.Game.Managers.Handlers
{
    public class AIHandler : DefaultHandler
    {
        public AIHandler(IDictionary<BoardCell, IPlayer> players, Action additionalAction) : base(players, additionalAction)
        {
            // do nothing
        }

        public override void PrepareGameBoard()
        {
            if (((AIPlayer)players[BoardCell.O]).PerformMove())
            {
                additionalAction.Invoke();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Players;

namespace TicTacToe.Data.Game.Managers.Handlers
{
    public class MultiplayerHandler : DefaultHandler
    {
        public MultiplayerHandler(IDictionary<BoardCell, IPlayer> players, Action additionalAction = null) : base(players, additionalAction)
        {
        }

        public override void PrepareGameBoard()
        {
            //TODO: discuss and figure out if additional preparations are necessary
            base.PrepareGameBoard();
        }
    }
}

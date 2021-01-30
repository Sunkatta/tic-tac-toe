using System;
using System.Collections.Generic;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Players;

namespace TicTacToe.Data.Game.Managers.Handlers
{
    public class DefaultHandler : IHandler
    {
        protected readonly IDictionary<BoardCell, IPlayer> players;

        protected readonly Action additionalAction;

        public DefaultHandler(IDictionary<BoardCell, IPlayer> players, Action additionalAction = null)
        {
            this.players = players;
            this.additionalAction = additionalAction;
        }

        public virtual void PrepareGameBoard()
        {
            // do nothing
        }
    }
}

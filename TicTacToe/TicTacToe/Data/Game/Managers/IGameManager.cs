using System.Collections.Generic;
using TicTacToe.Data.Game.Players;

namespace TicTacToe.Data.Game.Managers
{
    public interface IGameManager
    {
        IBoardManager BoardManager { get; }

        public List<IPlayer> Players { get; }

    }
}

using System.Collections.Generic;
using TicTacToe.Data.Game.Players;

namespace TicTacToe.Data.Game.Managers
{
    public class GameManager : IGameManager
    {
        private List<IPlayer> players = new List<IPlayer>();

        public IBoardManager BoardManager { get; private set; }

        public List<IPlayer> Players => players;
        /*
         * TODO: Figure out different keeping track of players' movenment, keep in mind you need to support local play vs another person and ai, maybe multi as well, should be scalable
         * 
         */


        public GameManager(GameContext context)
        {
            Players.AddRange(context.Players);
            BoardManager = new BoardManager();
        }
    }
}

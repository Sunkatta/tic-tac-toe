using System;
using System.Collections.Generic;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Players;
using TicTacToe.Data.Utils;

namespace TicTacToe.Data.Game
{
    public class GameContext
    {

        public GameMode Mode { get; private set; }

        public AIDifficulty Difficulty { get; private set; }

        public IList<IPlayer> Players { get; private set; }

        public int Dimensions { get; private set; }


        private GameContext()
        {
        }

        public class Builder
        {
            private GameMode Mode { get; set; }

            private AIDifficulty Difficulty { get; set; }

            public IList<IPlayer> Players { get; private set; }

            public int Dimensions { get; private set; }

            public Builder(GameMode mode)
            {
                Mode = mode;
            }

            public GameContext Build()
            {
                return new GameContext()
                {
                    Mode = Mode,
                    Difficulty = Difficulty,
                    Players = new List<IPlayer>()
                };
            }

            public Builder WithDifficulty(AIDifficulty difficulty)
            {
                if (Mode != GameMode.SP_AI)
                {
                    throw new ArgumentException(Constants.INCORRECT_GAME_MODE_ERROR_STRING);
                }
                Difficulty = difficulty;
                return this;
            }

            public Builder AddPlayer(IPlayer player)
            {
                if (Players.Count > 2)
                {
                    throw new InvalidOperationException();
                }
                Players.Add(player);
                return this;
            }

            public Builder WithDimensions(int dimensions)
            {
                if (dimensions < 3)
                {
                    throw new ArgumentException();
                }
                Dimensions = dimensions;
                return this;
            }
        }
    }
}

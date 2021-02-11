using System;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Utils;

namespace TicTacToe.Data.Game
{
    public class GameContext
    {

        public GameMode Mode { get; private set; }

        public AIDifficulty Difficulty { get; private set; }


        private GameContext()
        {
        }

        public class Builder
        {
            private GameMode Mode { get; set; }

            private AIDifficulty Difficulty { get; set; }

            public Builder(GameMode mode)
            {
                Mode = mode;
            }

            public GameContext Build()
            {
                return new GameContext()
                {
                    Mode = Mode,
                    Difficulty = Difficulty
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
        }
    }
}

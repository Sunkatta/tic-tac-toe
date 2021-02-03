using Microsoft.AspNetCore.Components;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Components
{
    public abstract class BasePlayerComponent : ComponentBase
    {
        public IGameManager GameManager { get; set; }

        public string EndGameTitle { get; set; }

        public string EndGameMessage { get; set; }

        public GameMode mode;

        public GameContext.Builder contextBuilder;

        public abstract void PlayAgain();
    }
}

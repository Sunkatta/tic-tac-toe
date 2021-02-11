using Microsoft.AspNetCore.Components;
using System;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Components
{
    public class BoardComponent : ComponentBase
    {
        [Parameter]
        public IGameManager GameManager { get; set; }

        [Parameter]
        public Action<int> OnTileClicked { get; set; }

        public AIDifficulty Difficulty { get; set; }

        [Parameter]
        public BoardCell PlayerCell { get; set; }

        protected override void OnInitialized()
        {
            GameManager.NewGame();
        }

        protected void ClickTile(BoardCell cell, int index)
        {
            if (!GameManager.IsGameFinished)
            {
                GameManager.ExecuteMove(cell, index);
            }

            OnTileClicked.Invoke(index);
        }

        protected void ClickTile(int index)
        {
            ClickTile(GameManager.PlayerTurn, index);
        }
    }
}

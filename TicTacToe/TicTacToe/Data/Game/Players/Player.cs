using System;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;
using TicTacToe.Data.Utils;

namespace TicTacToe.Data.Game.Players
{
    public class Player : IPlayer
    {
        private BoardCell playerCell;
        private IBoardManager boardManager;

        public BoardCell PlayerCell
        {
            get
            {
                return playerCell;
            }
            private set
            {
                if (value == BoardCell.EMPTY)
                {
                    throw new ArgumentException(Constants.INCORRECT_BOARD_CELL_SELECTED_ERROR_STRING);
                }
                playerCell = value;
            }
        }

        protected IBoardManager BoardManager
        {
            get
            {
                return boardManager;
            }
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException();
                }
                boardManager = value;
            }
        }

        public Player(BoardCell playerCell, IBoardManager boardManager)
        {
            BoardManager = boardManager;
            PlayerCell = playerCell;
        }


        public bool PerformMove(int index)
        {
            return boardManager.PerformMove(PlayerCell, index);
        }
    }
}

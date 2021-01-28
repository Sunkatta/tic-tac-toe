using System;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;
using TicTacToe.Data.Utils;

namespace TicTacToe.Data.Game.Players
{
    public class BasePlayer : IPlayer
    {
        private BoardCell playerCell;
        private BoardManager boardManager;

        public BoardCell PlayerCell
        {
            get
            {
                return playerCell;
            }
            set
            {
                if (value == BoardCell.EMPTY)
                {
                    throw new ArgumentException(Constants.INCORRECT_BOARD_CELL_SELECTED_ERROR_STRING);
                }
                playerCell = value;
            }
        }

        //TODO: Add some validation
        protected BoardManager BoardManager
        {
            get
            {
                return boardManager;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException();
                }
                boardManager = value;
            }
        }

        BoardManager IPlayer.BoardManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public BasePlayer(BoardManager boardManager, BoardCell playerCell)
        {
            BoardManager = boardManager;
            PlayerCell = playerCell;
        }


        public void PerformMove(int index)
        {
            boardManager.PerformMove(PlayerCell, index);
        }
    }
}

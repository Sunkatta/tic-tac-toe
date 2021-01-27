using System.Collections.Generic;
using System.Linq;
using TicTacToe.Data.Enums;

namespace TicTacToe.Data.Game
{
    public class GameManager : IGameManager
    {
        //TBD maybe add multiple dimensions
        private const int DIMENSIONS = 3;

        private BoardCell[] boardCells;

        public GameContext GameContext { get; private set; }

        public BoardCell Winner { get; private set; }

        //TODO  Rename to BoardManager, create GameManger which consists of BoardManager and player (ai or real person)


        public GameManager(GameContext context)
        {
            GameContext = context;
        }

        public bool HasWinner()
        {
            if (Winner != BoardCell.EMPTY)
            {
                return true;
            }
            return IsWinner(BoardCell.X) || IsWinner(BoardCell.O);
        }

        public bool IsFinished()
        {
            return HasWinner() || IsFull();
        }

        public BoardCell[] GetAvailableBoardCells()
        {
            return GetAllBoardCells().ToList().Where(x => x == BoardCell.EMPTY).ToArray();
        }

        public bool IsWinner(BoardCell cell)
        {
            bool result = CheckColumns(cell) || CheckRows(cell) || CheckDiagonals(cell);
            if (result)
            {
                Winner = cell;
            }
            return result;

        }

        private bool IsFull()
        {
            return !boardCells.Any(x => x == BoardCell.EMPTY);
        }

        private bool CheckRows(BoardCell cell)
        {
            List<BoardCell> cells = new List<BoardCell>();
            for (int i = 0; i < DIMENSIONS; i++)
            {
                int startRow = i * DIMENSIONS;
                for (int j = 0; j < DIMENSIONS; j++)
                {
                    cells.Add(boardCells[startRow + j]);
                }

                if (cells.All(x => x == cell))
                {
                    return true;
                }
                cells.Clear();
            }
            return false;
        }

        private bool CheckColumns(BoardCell cell)
        {
            List<BoardCell> cells = new List<BoardCell>();
            for (int i = 0; i < DIMENSIONS; i++)
            {
                for (int j = 0; j < DIMENSIONS; j++)
                {
                    cells.Add(boardCells[i + DIMENSIONS * j]);
                }

                if (cells.All(x => x == cell))
                {
                    return true;
                }
                cells.Clear();
            }
            return false;
        }

        private bool CheckDiagonals(BoardCell cell)
        {
            return CheckLeftDiagonal(cell) || CheckRightDiagonal(cell);
        }

        private bool CheckLeftDiagonal(BoardCell cell)
        {
            IList<BoardCell> cells = new List<BoardCell>();
            for (int i = 0; i < DIMENSIONS; i++)
            {
                cells.Add(GetAllBoardCells()[(DIMENSIONS + 1) * i]);
            }
            return cells.All(x => x == cell);
        }

        private bool CheckRightDiagonal(BoardCell cell)
        {
            IList<BoardCell> cells = new List<BoardCell>();
            for (int i = 1; i <= DIMENSIONS; i++)
            {
                cells.Add(GetAllBoardCells()[(DIMENSIONS - 1) * i]);
            }
            return cells.All(x => x == cell);
        }

        public BoardCell[] GetAllBoardCells()
        {
            return boardCells;
        }

        public BoardCell[] NewGame()
        {
            boardCells = new BoardCell[DIMENSIONS * DIMENSIONS];
            return boardCells;
        }
    }
}

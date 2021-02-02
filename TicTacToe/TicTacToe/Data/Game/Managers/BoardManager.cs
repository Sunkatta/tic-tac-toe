using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Data.Enums;

namespace TicTacToe.Data.Game.Managers
{
    public class BoardManager : IBoardManager
    {
        private const int DIMENSIONS = 3;

        private BoardCell[] boardCells;

        public BoardCell Winner { get; private set; }

        public int Dimensions { get; private set; }

        public BoardManager()
        {
            Dimensions = DIMENSIONS;
        }

        public BoardManager(int dimensions)
        {
            Dimensions = dimensions;
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

        public IList<int> GetEmptyBoardCellsIndexes()
        {
            IList<int> indexes = new List<int>();
            BoardCell[] boardCells = GetAllBoardCells();
            for (int i = 0; i < boardCells.Length; i++)
            {
                if (boardCells[i] == BoardCell.EMPTY)
                {
                    indexes.Add(i);
                }
            }
            return indexes;
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
            for (int i = 0; i < Dimensions; i++)
            {
                int startRow = i * Dimensions;
                for (int j = 0; j < Dimensions; j++)
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
            for (int i = 0; i < Dimensions; i++)
            {
                for (int j = 0; j < Dimensions; j++)
                {
                    cells.Add(boardCells[i + Dimensions * j]);
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
            for (int i = 0; i < Dimensions; i++)
            {
                cells.Add(GetAllBoardCells()[(Dimensions + 1) * i]);
            }
            return cells.All(x => x == cell);
        }

        private bool CheckRightDiagonal(BoardCell cell)
        {
            IList<BoardCell> cells = new List<BoardCell>();
            for (int i = 1; i <= Dimensions; i++)
            {
                cells.Add(GetAllBoardCells()[(Dimensions - 1) * i]);
            }
            return cells.All(x => x == cell);
        }

        public BoardCell[] GetAllBoardCells()
        {
            return boardCells;
        }

        public BoardCell[] NewBoard()
        {
            boardCells = new BoardCell[Dimensions * Dimensions];
            Winner = BoardCell.EMPTY;
            return boardCells;
        }

        public bool PerformMove(BoardCell playerCell, int index)
        {
            if (!IsFinished() && boardCells[index] == BoardCell.EMPTY)
            {
                boardCells[index] = playerCell;
                IsWinner(playerCell);
                return true;
            }
            return false;
        }
    }
}

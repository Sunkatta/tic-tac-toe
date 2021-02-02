using System.Collections.Generic;
using System.Linq;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers;

namespace TicTacToe.Data.Game.Players.AIStrategies
{
    public class HardAIStrategy : MediumAIStrategy
    {
        public HardAIStrategy(BoardCell aiCell, IBoardManager boardManager) : base(aiCell, boardManager)
        {
        }

        public override int GenerateMove()
        {
            // try to beat the player
            if (TryToMove(BoardCell.O, out int index))
            {
                return index;
            }

            // try to block the player
            if (TryToMove(BoardCell.X, out index))
            {
                return index;
            }

            // otherwise get one of the prefred moves
            return base.GenerateMove();
        }

        private bool TryToMove(BoardCell cell, out int result)
        {
            result = GetMove(cell, BoardManager.Dimensions);
            return result != -1;
        }

        private int GetIndexOfLinearBlock(BoardCell otherCell, int dimensions, bool isColumn)
        {
            Dictionary<int, BoardCell> cells = new Dictionary<int, BoardCell>();
            BoardCell[] boardCells = BoardManager.GetAllBoardCells();
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    if (isColumn)
                    {
                        cells.Add(i + dimensions * j, boardCells[i + dimensions * j]);
                    }
                    else
                    {
                        cells.Add(j + i * dimensions, boardCells[j + i * dimensions]);
                    }
                }
                if (HasToBlock(otherCell, dimensions, cells))
                {
                    return GetIndexOfBlock(cells);
                }
                cells.Clear();
            }
            return -1;
        }


        private int GetIndexOfDiagonalBlock(BoardCell otherCell, int dimensions, bool isLeft)
        {
            Dictionary<int, BoardCell> cells = new Dictionary<int, BoardCell>();
            BoardCell[] boardCells = BoardManager.GetAllBoardCells();
            int direction = isLeft ? 0 : 1;
            int addend = isLeft ? 1 : -1;

            for (int i = direction; i < dimensions + direction; i++)
            {
                cells.Add((dimensions + addend) * i, boardCells[(dimensions + addend) * i]);
            }

            if (HasToBlock(otherCell, dimensions, cells))
            {
                return GetIndexOfBlock(cells);
            }
            return -1;
        }


        private int GetIndexOfBlock(Dictionary<int, BoardCell> cells)
        {
            KeyValuePair<int, BoardCell> entry = cells.FirstOrDefault(e => e.Value == BoardCell.EMPTY);
            return entry.Equals(default(KeyValuePair<int, BoardCell>)) ? -1 : entry.Key;
        }

        private bool HasToBlock(BoardCell otherCell, int dimensions, Dictionary<int, BoardCell> cells)
        {
            return cells.Any(e => e.Value == BoardCell.EMPTY) && cells.Where(e => e.Value == otherCell).Count() == dimensions - 1;
        }

        private int GetMove(BoardCell piece, int dimensions)
        {
            SortedSet<int> indexes = new SortedSet<int>
            {
                GetIndexOfLinearBlock(piece, dimensions, true),
                GetIndexOfLinearBlock(piece, dimensions, false),
                GetIndexOfDiagonalBlock(piece, dimensions, true),
                GetIndexOfDiagonalBlock(piece, dimensions, false)
            };
            return indexes.Reverse().First();
        }
    }
}

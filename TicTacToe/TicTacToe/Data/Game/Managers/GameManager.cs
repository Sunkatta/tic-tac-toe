using System.Collections.Generic;
using TicTacToe.Data.Enums;
using TicTacToe.Data.Game.Managers.Handlers;
using TicTacToe.Data.Game.Players;

namespace TicTacToe.Data.Game.Managers
{
    public class GameManager : IGameManager
    {
        private readonly IDictionary<BoardCell, IPlayer> players = new Dictionary<BoardCell, IPlayer>
        {
            { BoardCell.X, null},
            { BoardCell.O, null}
        };

        public GameContext Context { get; private set; }

        public IBoardManager BoardManager { get; }

        public BoardCell PlayerTurn { get; private set; }

        public BoardCell Winner => BoardManager.Winner;

        public bool IsGameFinished => BoardManager.IsFinished();

        private IHandler handler;

        public GameManager(GameContext context)
        {
            BoardManager = new BoardManager();
            Context = context;
            PlayerTurn = BoardCell.X;
            players[BoardCell.X] = new Player(BoardCell.X, BoardManager);
            DetermineGameMode();
        }

        public void ExecuteMove(int index)
        {
            if (players[PlayerTurn].PerformMove(index))
            {
                UpdatePlayerTurn();
                handler.PrepareGameBoard();
            }
        }

        public void ExecuteMove(BoardCell playerCell, int index)
        {
            if (PlayerTurn == playerCell && players[PlayerTurn].PerformMove(index))
            {
                UpdatePlayerTurn(playerCell);
                handler.PrepareGameBoard();
            }
        }

        public BoardCell[] NewGame()
        {
            BoardCell[] newBoard = BoardManager.NewBoard();
            if (PlayerTurn == BoardCell.O)
            {
                handler.PrepareGameBoard();
            }
            return newBoard;
        }

        private void UpdatePlayerTurn(BoardCell playerCell)
        {
            bool isX = playerCell == BoardCell.X;
            PlayerTurn = isX ? BoardCell.O : BoardCell.X;
        }

        private void UpdatePlayerTurn()
        {
            UpdatePlayerTurn(PlayerTurn);
        }

        private void DetermineGameMode()
        {
            switch (Context.Mode)
            {
                case GameMode.SP_AI:
                    handler = new AIHandler(players, () => UpdatePlayerTurn());
                    players[BoardCell.O] = new AIPlayer(BoardCell.O, BoardManager, Context.Difficulty);
                    break;
                default:
                    handler = new DefaultHandler(players);
                    players[BoardCell.O] = new Player(BoardCell.O, BoardManager);
                    break;
            }
        }
    }
}

using System;
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

        private readonly GameContext context;

        public IBoardManager BoardManager { get; }

        public BoardCell PlayerTurn { get; private set; }

        public BoardCell Winner => BoardManager.Winner;

        public bool IsGameFinished => BoardManager.IsFinished();

        private readonly IHandler handler;

        //TODO: needs refactoring
        public GameManager(GameContext context)
        {
            BoardManager = new BoardManager();
            this.context = context;
            PlayerTurn = BoardCell.X;
            players[BoardCell.X] = new Player(BoardCell.X, BoardManager);
            switch (context.Mode)
            {
                case GameMode.SP_AI:
                    handler = new AIHandler(players, () => UpdatePlayerTurn());
                    players[BoardCell.O] = new AIPlayer(BoardCell.O, BoardManager, context.Difficulty);
                    break;
                default://TODO: needs to be revised when multiplayer is introduced
                    handler = new DefaultHandler(players);
                    players[BoardCell.O] = new Player(BoardCell.O, BoardManager);
                    break;
            }
        }

        public void ExecuteMove(int index)
        {
            if (players[PlayerTurn].PerformMove(index))
            {
                UpdatePlayerTurn();
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

        private void UpdatePlayerTurn()
        {
            bool isX = PlayerTurn == BoardCell.X;
            PlayerTurn = isX ? BoardCell.O : BoardCell.X;
        }
    }
}

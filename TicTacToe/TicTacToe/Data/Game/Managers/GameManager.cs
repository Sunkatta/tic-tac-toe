using System.Collections.Generic;
using TicTacToe.Data.Enums;
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

        private readonly IBoardManager boardManager;

        public BoardCell PlayerTurn { get; private set; }

        public BoardCell Winner => boardManager.Winner;

        public bool IsGameFinished => boardManager.IsFinished();

        public GameManager(GameContext context)
        {
            PlayerTurn = BoardCell.X;
            boardManager = new BoardManager();
            this.context = context;
            players[BoardCell.X] = new Player(BoardCell.X, boardManager);
            players[BoardCell.O] = CreateOpponent(this.context);
        }

        public void ExecuteMove(int index)
        {
            //TODO: Needs to be fixed because currently it is not possible to have the AI player go first
            if (players[PlayerTurn].PerformMove(index))
            {
                UpdatePlayerTurn();
                if (context.Mode == GameMode.SP_AI)
                {
                    ((AIPlayer)players[PlayerTurn]).PerformMove();
                    UpdatePlayerTurn();
                }
            }
        }

        private IPlayer CreateOpponent(GameContext context)
        {
            switch (context.Mode)
            {
                case GameMode.SP_AI:
                    return new AIPlayer(BoardCell.O, boardManager, context.Difficulty);
                default:
                    return new Player(BoardCell.O, boardManager);
            }
        }

        public BoardCell[] NewGame()
        {
            return boardManager.NewBoard();
        }

        private void UpdatePlayerTurn()
        {
            bool isX = PlayerTurn == BoardCell.X;
            PlayerTurn = isX ? BoardCell.O : BoardCell.X;
        }
    }
}

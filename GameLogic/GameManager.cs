using System;
using System.Collections.Generic;

namespace BackEnd
{
    public class GameManager
    {
        private Player  m_Player1;
        private Player  m_Player2;
        public Player   CurrPlayer { get; private set; }
        public Player   LastPlayer { get; private set; }
        public Move     LastMove { get; private set; }
        public Board    GameBoard { get; private set; }

        public GameManager(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            Player1 = i_Player1;
            Player2 = i_Player2;
            GameBoard = new Board(i_BoardSize);
            CurrPlayer = Player1;
            LastPlayer = Player2;
        }
        public Player Player1
        {
            get { return m_Player1; }
            private set { m_Player1 = value; }
        }

        public Player Player2
        {
            get { return m_Player2; }
            private set { m_Player2 = value; }
        }
        public void QuitGame()
        {
            Console.WriteLine("GameManager: quitting game");
        }

        public void EndRound()
        {
            updatePlayersPoints();
        }

        public void StartNewRound(int i_BoardSize)
        {
            GameBoard = new Board(i_BoardSize);
            CurrPlayer = Player1;
            LastPlayer = Player2;
            LastMove = new Move();
        }

        public void AddPlayer(Player i_Player)
        {
            Console.WriteLine("GameManager: new player was added");
        }
        
        private bool isAnotherMoveAfterEat()
        {
            return (CurrPlayer.PlayerType == LastPlayer.PlayerType);
        }

        public bool MovePiece(Move i_Move)
        {
            bool anotherMove, isValidMove;

            if (CurrPlayer.r_IsPc)
            {
                if (isAnotherMoveAfterEat())
                {
                    getRandomMove(LastMove.DestinationPos, out i_Move);
                }
                else
                {
                    getRandomMove(null, out i_Move);
                }
            }

            if (!isAnotherMoveAfterEat())
            {
                isValidMove = GameBoard.MovePiece(i_Move, CurrPlayer.PlayerType, out anotherMove, null);
            }
            else
            {
                isValidMove = GameBoard.MovePiece(i_Move, CurrPlayer.PlayerType, out anotherMove, LastMove.DestinationPos);
            }
            if (isValidMove)
            {
                LastMove = i_Move;
                LastPlayer = CurrPlayer;
                if(anotherMove)
                {
                    CurrPlayer = (CurrPlayer.PlayerType == Player1.PlayerType) ? Player1 : Player2;
                }
                else
                {
                    CurrPlayer = (CurrPlayer.PlayerType == Player1.PlayerType) ? Player2 : Player1;
                }
            }

            return isValidMove;
        }
        private bool getRandomMove(Position? i_FromPos, out Move o_RandomMove)
        {
            bool isSucceed = false;
            o_RandomMove = new Move();
            List<Move> eatingMoves = new List<Move>();
            List<Move> regularMoves = new List<Move>();

            if (i_FromPos.HasValue)
            {
                Piece? fromPiece = GameBoard.GetPeiceFromBoard(i_FromPos.Value);
                if (fromPiece.HasValue)
                {
                    GameBoard.GetSinglePoneMovements(fromPiece.Value, eatingMoves, regularMoves);
                    isSucceed = eatingMoves.Count>0 || regularMoves.Count>0 ? true : false;
                }
            }
            else
            {
                GameBoard.GetAllPonesMovements(CurrPlayer.PlayerType, eatingMoves, regularMoves);
                isSucceed = eatingMoves.Count > 0 || regularMoves.Count > 0 ? true : false;
            }
            if (isSucceed)
            {
                o_RandomMove = (eatingMoves.Count>0)? getRandomItemFromList(eatingMoves) : getRandomItemFromList(regularMoves);
            }

            return isSucceed;
        }

        private T getRandomItemFromList<T>(List<T> i_List)
        {
            Random random = new Random();

            return i_List[random.Next(i_List.Count)];
        }

        private void updatePlayersPoints()
        {
            int player1Points = GameBoard.GetNumOfPointsByPlayer(Player1.PlayerType);
            int player2Points = GameBoard.GetNumOfPointsByPlayer(Player2.PlayerType);

            int player1AdditionalPoints = player1Points - player2Points;
            if (player1AdditionalPoints > 0)
            {
                m_Player1.Points += player1AdditionalPoints;
            }
            else
            {
                m_Player2.Points += Math.Abs(player1AdditionalPoints);
            }
        }

        //function to check which one of the player lose
        public Player WhichPlayerWonAfterGameOverOneLose()
        {
            Player winningPlayer = (GameBoard.GetAllPieces(Player1.PlayerType).Count == 0) ? Player2 : Player1;
            
            return winningPlayer;
        }

        // fuction to check if the game need to be over because one of them lose
        public bool CheckIfSomeoneLoseAllPieces()
        {
            bool isSomeoneLose = false;
            if((GameBoard.GetAllPieces(Player1.PlayerType).Count == 0) || (GameBoard.GetAllPieces(Player2.PlayerType).Count == 0))
            {
                isSomeoneLose = true;
            }
            return isSomeoneLose;
        }

        public bool CheckIfTheresNoOptionToMove()
        {
            bool noOptionToMove = false;
            List<Move> eatingMovesPlayerCurr = new List<Move>();
            List<Move> regularMovesPlayerCurr = new List<Move>();
            List<Move> eatingMovesPlayerLast = new List<Move>();
            List<Move> regularMovesPlayerLast = new List<Move>();
            GameBoard.GetAllPonesMovements(Player1.PlayerType, eatingMovesPlayerCurr, regularMovesPlayerCurr);
            GameBoard.GetAllPonesMovements(Player2.PlayerType, eatingMovesPlayerLast, regularMovesPlayerLast);
            if ((eatingMovesPlayerCurr.Count == 0) && (regularMovesPlayerCurr.Count == 0) && (eatingMovesPlayerLast.Count == 0) && (regularMovesPlayerLast.Count == 0))
            {
                noOptionToMove = true;
            }
            return noOptionToMove;

        }
    }
    

}


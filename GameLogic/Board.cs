using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd
{

    public enum eMoveType
    {
        Regular,
        Eat,
        NotValidMove
    }
    public class Board
    {
        private Piece?[,]   m_Board;
        public int          Size { get; private set; }

        public Board(int i_Size)
        {
            Size = i_Size;
            m_Board = new Piece?[i_Size, i_Size];
            //initBoard();
            initializeBoard();
        }
        public string GetRowSymbols(int i_Row)
        {
            StringBuilder rowOfSymbols = new StringBuilder(new string(' ', Size));

            for (int i = 0; i < Size; i++)
            {
                if(m_Board[i_Row, i] != null)
                {
                    rowOfSymbols.Insert(i, m_Board[i_Row, i].Value.Symbol.ToString());
                }
            }

            return rowOfSymbols.ToString();
        }

        public void GetAllPonesMovements(ePlayerType i_PlayerType, List<Move> o_EatMoves, List<Move> o_RegularMoves)
        {
            ///this function will check all the moves all pieces can do
            ///and return them as a list
            List<Piece> piecesList = GetAllPieces(i_PlayerType); 

            foreach (Piece piece in piecesList) 
            {
                GetSinglePoneMovements(piece, o_EatMoves, o_RegularMoves);
            }
        }

        public void GetSinglePoneMovements( Piece i_Piece,
                                                List<Move> o_EatMoves,
                                                List<Move> o_RegularMoves)
        {
            ///this function returns all the eating moves a specific piece can do
            switch (i_Piece.Direction) 
            {

                case ePieceDirection.KingAnywhere:

                    appendNextPieceMove(i_Piece, o_EatMoves, o_RegularMoves, new Position(-1, -1));
                    appendNextPieceMove(i_Piece, o_EatMoves, o_RegularMoves, new Position(-1, 1));
                    appendNextPieceMove(i_Piece, o_EatMoves, o_RegularMoves, new Position(1, -1));
                    appendNextPieceMove(i_Piece, o_EatMoves, o_RegularMoves, new Position(1, 1));
                    break;
                case ePieceDirection.Up:
                    appendNextPieceMove(i_Piece, o_EatMoves, o_RegularMoves, new Position(-1, -1));
                    appendNextPieceMove(i_Piece, o_EatMoves, o_RegularMoves, new Position(-1, 1));
                    break;
                case ePieceDirection.Down:
                    appendNextPieceMove(i_Piece, o_EatMoves, o_RegularMoves, new Position(1, -1));
                    appendNextPieceMove(i_Piece, o_EatMoves, o_RegularMoves, new Position(1, 1));
                    break;
            }
        }

        private void appendNextPieceMove(Piece i_Piece,
                                        List<Move> o_EatMoves,
                                        List<Move> o_RegularMoves,
                                        Position i_MoveDelta)
        {
            
            Position newPos = new Position(i_Piece.PiecePosition.Row + i_MoveDelta.Row, i_Piece.PiecePosition.Col + i_MoveDelta.Col);
            Position afterNewPosInSameDirection = new Position(newPos.Row + i_MoveDelta.Row, newPos.Col + i_MoveDelta.Col);

            //check if there is a valid move to the desired direction
            if (!checkIfPositionFree(newPos) &&
                (GetPeiceFromBoard(newPos) != null) &&
                !CheckIfSameGroupMembers(GetPeiceFromBoard(newPos).Value, i_Piece) &&
                checkIfPositionFree(afterNewPosInSameDirection))
            {
                o_EatMoves.Add(new Move(i_Piece.PiecePosition, afterNewPosInSameDirection));
            }
            else if (checkIfPositionFree(newPos))
            {
                o_RegularMoves.Add(new Move(i_Piece.PiecePosition, newPos));
            }
        }

        public bool CheckIfSameGroupMembers(Piece i_Piece1, Piece i_Piece2)
        {
            return i_Piece1.Player == i_Piece2.Player;
        }

      
        public List<Piece> GetAllPieces(ePlayerType i_PlayerType)
        {
            List<Piece> allPieces = new List<Piece>();

            foreach (Piece? piece in m_Board)
            {
                if(piece.HasValue && piece.Value.Player == i_PlayerType)
                {
                    allPieces.Add(piece.Value);
                }
            }

            return allPieces;
        }
        
        private ePieceSymbol eatPieceInsideMove(Move i_Move)
        {
            ///Extract a game piece which is between two places (move)
            int eatenPieceRow = (i_Move.StartPos.Row + i_Move.DestinationPos.Row)/2;
            int eatenPieceCol = (i_Move.StartPos.Col + i_Move.DestinationPos.Col)/2;

            Piece? eatenPiece;
            extractPeiceFromBoard(new Position(eatenPieceRow, eatenPieceCol), out eatenPiece);

            return eatenPiece.Value.Symbol;
        }

        public bool MovePiece(Move i_Move, ePlayerType i_PlayerType, out bool o_AnotherMove, Position? i_FromPos)
        {
            /*this function checks if a pone can move inside the board
             * if it cant, false will return from the function and nothing will happen
             */
            bool isValidMove = true;
            eMoveType moveType;
            List<Move> nextEatMoves = new List<Move>(), nextRegularMoves = new List<Move>();
            o_AnotherMove = false;

            bool isBecomeKing;

            isValidMove = checkMove(i_Move, i_PlayerType, out moveType, i_FromPos);

            if (isValidMove) 
            {
                Piece? nullablePiece;
                isValidMove &= extractPeiceFromBoard(i_Move.StartPos, out nullablePiece);
                if (isValidMove)
                {
                    Piece piece = (Piece)nullablePiece;
                    piece.PiecePosition = i_Move.DestinationPos;
                    isValidMove = insertPiece(piece);
                    if (isValidMove && (moveType == eMoveType.Eat))
                    {
                        eatPieceInsideMove(i_Move);
                        GetSinglePoneMovements((Piece)GetPeiceFromBoard(i_Move.DestinationPos), nextEatMoves, nextRegularMoves);
                        o_AnotherMove = (nextEatMoves.Count > 0);
                    }
                    isBecomeKing = checkIfBecomeKingAfterMove(piece);
                    if (isBecomeKing)
                    {
                        isValidMove &= promotePieceToKing(piece);
                    }
                }
            }

            return isValidMove;
        }

        private bool promotePieceToKing(Piece piece)
        {
            bool res = false;
            Piece? toKingPiece;

            extractPeiceFromBoard(piece.PiecePosition, out toKingPiece);
            if (toKingPiece.HasValue)
            {
                Piece newPiece = (Piece)toKingPiece;
                newPiece.PromoteToKing();
               
                insertPiece(newPiece);
                res = true;
            }

            return res;
        }

        private bool checkMove( Move i_Move,
                                ePlayerType i_PlayerType,
                                out eMoveType o_MoveType,
                                Position? FromPos)
        {
            ///this function checks the movment a player wants to play
            ///if there is an option to "eat" an openent piece, this kind of move must be played
            ///otherwise a regular move will be valid and regular move check will occur
            bool isValid = true;
            o_MoveType = eMoveType.NotValidMove;
            List<Move> avaliableEatingMoves = new List<Move>();
            List<Move> avaliableRegularMoves = new List<Move>();

            if (FromPos.HasValue)
            {
                Piece? piece = GetPeiceFromBoard(FromPos.Value);
                if (piece.HasValue)
                {
                    GetSinglePoneMovements((Piece)piece, avaliableEatingMoves, avaliableRegularMoves);
                }else
                {
                    isValid = false;
                }
            }else
            {
                GetAllPonesMovements(i_PlayerType, avaliableEatingMoves, avaliableRegularMoves);
            }
            if (isValid)
            {
                o_MoveType = (avaliableEatingMoves.Count > 0) ? eMoveType.Eat : eMoveType.Regular;
                if ((o_MoveType == eMoveType.Eat) && !avaliableEatingMoves.Contains(i_Move))
                {
                    isValid = false;
                }else if ((avaliableEatingMoves.Count == 0) && !avaliableRegularMoves.Contains(i_Move))
                {
                    isValid = false;
                }
            }
            
            return isValid;
        }

        private bool checkIfPositionFree(Position i_Pos)
        {
            ///return false if the position is taken or not exist,
            ///true if the position exist and free from other pieces
            bool isFree;

            isFree = checkIfPositionInBoard(i_Pos) && (m_Board[i_Pos.Row, i_Pos.Col] == null);

            return isFree;
        }

        private bool checkIfPositionInBoard(Position i_Pos)
        {
            bool isValid = true;

            if (i_Pos.Row < 0 || i_Pos.Row >= Size || i_Pos.Col < 0 || i_Pos.Col >= Size)
            {
                isValid = false;
            }

            return isValid;
        }

        private bool insertPiece(Piece i_Piece)
        {
            ///insert a piece in the board
            ///if could not do the action, a false will return
            bool canInsert = true;
            Position pos = i_Piece.PiecePosition;

            canInsert = checkIfPositionInBoard(pos);
            //need to check if there is a pone in that location already
            if (canInsert)
            {
                m_Board[pos.Row, pos.Col] = i_Piece;
            }

            return canInsert;
        }

        private bool extractPeiceFromBoard(Position i_Pos, out Piece? o_Piece)
        {
            ///this functioin take a pone from the board and return it as an output 
            ///variable, bool for success/fail action will be returned
            ///good for moving a pone in the board or when a pone is eaten
            bool isPieceExist = false;
            
            o_Piece = m_Board[i_Pos.Row, i_Pos.Col];
            if (o_Piece != null)
            {
                
                m_Board[i_Pos.Row, i_Pos.Col] = null;
                isPieceExist = true;
            }

            return isPieceExist;
        }

        public Piece? GetPeiceFromBoard(Position i_Pos)
        {
            ///this function returns a copy of a piece or null from the board for read only matters
            Piece? piece = null;

            if (checkIfPositionInBoard(i_Pos))
            {
                piece = m_Board[i_Pos.Row, i_Pos.Col];
            }
            
            return piece;
        }

        
        private void initializeBoard()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (row < (Size / 2) - 1 && (row + col) % 2 != 0)
                    {
                        insertPiece(new Piece(ePieceSymbol.O, new Position(row, col), ePlayerType.White));
                    }
                    else if (row >= (Size / 2) + 1 && (row + col) % 2 != 0)
                    {
                        insertPiece(new Piece(ePieceSymbol.X, new Position(row, col), ePlayerType.Black));
                    }
                }
            }
        }

        private bool checkIfBecomeKingAfterMove(Piece i_Piece)
        {
            bool isKing = false;
            if(i_Piece.PiecePosition.Row == 0 || i_Piece.PiecePosition.Row == Size-1)
            {
                isKing = true;
            }
           
            return isKing;
        }
        public int GetNumOfPointsByPlayer(ePlayerType i_Player)
        {
            int points = 0;
            List<Piece> pieces = GetAllPieces(i_Player);

            foreach( Piece piece in pieces )
            {
                points += piece.PointsValue;
;           }

            return points;
        }
    }

}
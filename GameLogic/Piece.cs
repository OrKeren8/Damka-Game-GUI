using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace BackEnd
{
    public enum ePieceDirection
    {
        Down,
        Up,
        KingAnywhere
    }

    public enum ePieceSymbol
    {
        O,
        X,
        U,
        K
    }

    public struct Piece
    {

        private const int           k_RegularPiecePoints = 1;
        private const int           k_KingPiecePoints = 4;
        private Position            m_Position;
        private bool                m_IsKing;
        private ePieceSymbol        m_Symbol;
        private ePieceDirection     m_PoneDirection;
        private ePlayerType         m_Player;
        private int                 m_PointsValue;

        public Piece(ePieceSymbol i_Symbol, Position i_Position, ePlayerType i_Player)
        {
            m_Symbol = i_Symbol;
            m_Position = i_Position;
            m_IsKing = false;
            m_Player = i_Player;
            m_PointsValue = k_RegularPiecePoints;

            switch (m_Symbol)
            {
                case ePieceSymbol.O:
                    m_PoneDirection = ePieceDirection.Down;
                    break;
                case ePieceSymbol.X:
                    m_PoneDirection = ePieceDirection.Up;
                    break;
                default:
                    m_PoneDirection = ePieceDirection.KingAnywhere;
                    break;
            }
        }

        public ePieceDirection Direction
        {
            get { return m_PoneDirection; }
        }

        public int PointsValue
        {
            get { return m_PointsValue; }
        }

        public ePlayerType Player
        {
            get { return m_Player; }
        }

        public Position PiecePosition
        {
            get {return m_Position;}
            set { m_Position = value;}
        }
        public bool IsKing
        {
            get { return m_IsKing; }
            
        }

        public ePieceSymbol Symbol
        {
            get { return m_Symbol; }
        }

        public void PromoteToKing()
        {
            m_IsKing = true;
            m_PointsValue = k_KingPiecePoints;
            if(m_Symbol == ePieceSymbol.O)
            {
                m_Symbol = ePieceSymbol.U;
            }
            else if(m_Symbol == ePieceSymbol.X)
            {
                m_Symbol = ePieceSymbol.K;
            }
            m_PoneDirection = ePieceDirection.KingAnywhere;
        }
    }
}

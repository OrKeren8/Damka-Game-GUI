using System;

namespace BackEnd
{
    public enum ePlayerType
    {
        White,
        Black
    }

    public struct Player
    {
        private string                  m_Name;
        public readonly bool            r_IsPc; //in order to match property naming style
        private readonly ePlayerType    r_PlayerType;
        private int                     m_Points;

        public string Name
        {
            get { return m_Name; }
        }

        //constructor
        public Player(string i_Name, bool i_IsPc, ePlayerType i_PlayerType)
        {
            m_Name = i_Name;
            r_IsPc = i_IsPc;
            r_PlayerType = i_PlayerType;
            m_Points = 0;
        }

        public int Points
        {
            get { return m_Points; }
            set { m_Points = value; }
        }

        public ePlayerType PlayerType
        {
            get { return r_PlayerType; }
        }
    }
}

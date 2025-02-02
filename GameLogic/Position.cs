
using System;

namespace BackEnd
{
    public struct Position
    {
        public int  Row { get; set; }
        public int  Col { get; set; }

        public Position(int i_Row, int i_Col)
        {
            Row = i_Row;
            Col = i_Col;
        }
    }

    public struct Move
    {
        public Position StartPos { get; private set; }
        public Position DestinationPos { get; private set; }

        public Move(Position i_StartPos, Position i_DestionationPos)
        {
            StartPos = i_StartPos;
            DestinationPos = i_DestionationPos;
        }

        public override string ToString()
        {
            return $"{(char)('A' + StartPos.Row)}{(char)('A' + StartPos.Col)}>{(char)('A' + DestinationPos.Row)}{(char)('A' + DestinationPos.Col)}";
        }

        public static explicit operator Move(string input)
        {
            Move move;

            if(input == "")
            {
                move = new Move();
            }
            else
            {
                string[] parts = input.Split('>');
                if (parts.Length != 2)
                {
                    throw new FormatException("Input string must be in the format 'Aa'");
                }
                //Aa>Bb
                Position startPos = new Position(parts[0][1] - 'a', parts[0][0] - 'A');
                Position destPos = new Position(parts[1][1] - 'a', parts[1][0] - 'A');

                move = new Move(startPos, destPos);
            }
            
            return move;
        }
    }
}

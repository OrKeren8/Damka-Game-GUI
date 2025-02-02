using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackEnd;

namespace DamkaGUI
{
    internal class DamkaBoardButton : Button
    {
        private readonly int r_CubeSize = 50;

        public Position Pos {get; private set;}

        public DamkaBoardButton(bool i_State, char i_Symbol, Position i_Pos)
        {
            Pos = i_Pos;
            this.initButton(i_State, i_Symbol, i_Pos);
        }
        private void initButton(bool i_State, char i_Symbol, Position i_Pos)
        {
            this.Height = this.Width = this.r_CubeSize;
            this.Text = i_Symbol.ToString();
            int xPos = this.r_CubeSize * (i_Pos.Col + 1);
            int yPos = this.r_CubeSize * (i_Pos.Row + 1);

            this.Location = new Point(xPos, yPos);
            this.Enabled = i_State;
            this.BackColor = i_State ? Color.White : Color.DimGray;
        }
    }
}

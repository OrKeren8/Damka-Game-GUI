using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BackEnd;
using System.ComponentModel;
using static System.Windows.Forms.AxHost;

namespace DamkaGUI
{
    internal class FormDamka : Form
    {
        private readonly int r_CubeSize = 50;

        private FormSettings.Settings Settings { get; set; }
        private GameManager DamkaManager { get; set; }
        private Button[,] BoardButtons { get; set; }

        public void AddSettings(FormSettings.Settings i_Settings)
        {
            this.Settings = i_Settings;
            this.BoardButtons = new Button[i_Settings.boardSize, i_Settings.boardSize];
            this.initDamkaManager();
            this.initBoardComponents(this.DamkaManager.GameBoard);
        }

        private void initDamkaManager()
        {
            Player player1 = new Player(this.Settings.firstPlayerName, i_IsPc: false, ePlayerType.White);
            Player player2 = new Player(this.Settings.secondPlayerName, this.Settings.isSecondPlayerPC, ePlayerType.Black);
            this.DamkaManager = new GameManager(player1, player2, this.Settings.boardSize);
        }

        private void gameMaimLoop()
        {
            bool isFinished = false;

        }

        private void initBoardComponents(Board i_Board)
        {
            for (int row = 0; row < i_Board.Size; row++)
            {
                for (int col = 0; col < i_Board.Size; col++)
                {
                    this.createBoardButton(((row + col) % 2 != 0), i_Board.GetRowSymbols(row)[col], new Position(row, col));
                }
            }
        }

        private void createBoardButton(bool i_State, char i_Symbol, Position i_Pos)
        {
            Button boardButton = new Button();
            boardButton.Height = boardButton.Width = this.r_CubeSize;
            boardButton.Text = i_Symbol.ToString();
            int xPos = this.r_CubeSize * (i_Pos.Col+1);
            int yPos = this.r_CubeSize * (i_Pos.Row+1);
            
            boardButton.Location = new Point(xPos, yPos);
            boardButton.Enabled = i_State;
            boardButton.BackColor = i_State? Color.White : Color.DimGray;
            boardButton.Click += new EventHandler(this.boardButton_Click);
            this.BoardButtons[i_Pos.Row, i_Pos.Col] = boardButton;
            this.Controls.Add(boardButton);
        }

        private void boardButton_Click(object i_Sender, EventArgs i_Args)
        {

        }
    }
}

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

        private FormSettings.Settings Settings { get; set; }
        private GameManager DamkaManager { get; set; }
        private Button[,] BoardButtons { get; set; }

        private Button ClickedButton { get; set; } = null;

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
                    DamkaBoardButton boardButton = new DamkaBoardButton(((row + col) % 2 != 0), i_Board.GetRowSymbols(row)[col], new Position(row, col));
                    boardButton.Click += new EventHandler(this.boardButton_Click);
                    this.BoardButtons[row, col] = boardButton;
                    this.Controls.Add(boardButton);
                }
            }
        }

        private void boardButton_Click(object i_Sender, EventArgs i_Args)
        {
            if(this.ClickedButton != null)
            {
                this.ClickedButton = (i_Sender as Button);
            }
            else
            {
                //this.DamkaManager.MovePiece()
            }
        }
    }
}

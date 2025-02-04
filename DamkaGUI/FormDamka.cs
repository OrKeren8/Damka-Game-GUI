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
        private DamkaBoardButton[,] BoardButtons { get; set; }
        private DamkaBoardButton ClickedButton { get; set; } = null;

        private ScoreLabel Player1ScoreLabel { get; set; } = new ScoreLabel();
        private ScoreLabel Player2ScoreLabel { get; set; } = new ScoreLabel();


        public void AddSettings(FormSettings.Settings i_Settings)
        {
            this.Settings = i_Settings;
            this.BoardButtons = new DamkaBoardButton[i_Settings.boardSize, i_Settings.boardSize];
            this.initDamkaManager();
            this.initBoardComponents(this.DamkaManager.GameBoard);
            this.initScoreLables();
            DamkaBoardButton bottomRightButton = this.BoardButtons[this.Settings.boardSize-1, this.Settings.boardSize-1];
            this.ClientSize = new Size(
                bottomRightButton.Right + bottomRightButton.Width,
                bottomRightButton.Bottom + bottomRightButton.Height);
        }

        private void initDamkaManager()
        {
            Player player1 = new Player(this.Settings.firstPlayerName, i_IsPc: false, ePlayerType.White);
            Player player2 = new Player(this.Settings.secondPlayerName, this.Settings.isSecondPlayerPC, ePlayerType.Black);
            this.DamkaManager = new GameManager(player1, player2, this.Settings.boardSize);
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

        private void refreshBoard()
        {
            for (int row = 0; row < this.DamkaManager.GameBoard.Size; row++)
            {
                for (int col = 0; col < this.DamkaManager.GameBoard.Size; col++)
                {
                    this.BoardButtons[row, col].Text = this.DamkaManager.GameBoard.GetRowSymbols(row)[col].ToString();
                }
            }
        }

        private void boardButton_Click(object i_Sender, EventArgs i_Args)
        {
            if(this.ClickedButton == null)
            {
                (i_Sender as DamkaBoardButton).BackColor = Color.AntiqueWhite;
                this.ClickedButton = (i_Sender as DamkaBoardButton);
            }
            else
            {
                if(this.ClickedButton == (i_Sender as DamkaBoardButton))
                {
                    this.ClickedButton = null;
                    (i_Sender as DamkaBoardButton).BackColor = Color.White;
                }
                else
                {
                    bool succeed = this.DamkaManager.MovePiece(new Move(this.ClickedButton.Pos, (i_Sender as DamkaBoardButton).Pos));
                    if (succeed)
                    {
                        this.refreshBoard();
                        this.ClickedButton.BackColor = Color.White;
                        this.ClickedButton = null;
                    }
                }
            }
        }

        private void initScoreLables()
        {
            int firstXOffset = this.BoardButtons[0, 1].Left;
            initSingleScoreLabel(this.Player1ScoreLabel, this.Settings.firstPlayerName, new Point(firstXOffset, 25));
            int secondXOffset = this.BoardButtons[0, this.Settings.boardSize / 2 + 1].Left;
            initSingleScoreLabel(this.Player2ScoreLabel, this.Settings.secondPlayerName, new Point(secondXOffset, 25));
        }

        private void initSingleScoreLabel(ScoreLabel i_ScoreLabel, string i_Name, Point i_Location)
        {
            string name = $"{i_Name}: {i_ScoreLabel.Score}";
            i_ScoreLabel.Text = name;
            i_ScoreLabel.Location = i_Location;
            this.Controls.Add(i_ScoreLabel);
        }
    }
}

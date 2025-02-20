using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;


namespace DamkaGUI
{
    internal class FormSettings : Form
    {
        public struct Settings
        {
            public int m_BoardSize;
            public string m_FirstPlayerName;
            public string m_SecondPlayerName;
            public bool m_IsSecondPlayerPC;
        }

        public bool m_IsInistializedProperly { get; private set; } = false;
        private readonly int r_VerticalMargin = 5;
        private int CurrentVerticalMargin { get; set; } = 20;
        private readonly int r_HorizontalMargin = 25;
        private readonly string r_Player2DefaultName = "[Computer]";
        private Label LabelBoardSize { get; set; } = new Label();
        private Dictionary<string, RadioButton> RadioButtonsBoardSize { get; set; } = new Dictionary<string, RadioButton>();
        private Label LabelPlayers { get; set; } = new Label();
        private Label LabelPlayer1 { get; set; } = new Label();
        private CheckBox CheckBoxPlayer2 { get; set; } = new CheckBox();
        private TextBox Player1Name {get; set; } = new TextBox();
        private TextBox Player2Name {get; set; } = new TextBox();
        private Button ButtonFinish {get; set; } = new Button();

        public FormSettings()
        {
            this.Size = new Size(230, 250);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "GameSettings";
        }

        public Settings GetSettings()
        {
            Settings settings = new Settings
                                    { 
                                        m_BoardSize = this.getBoardSize(),
                                        m_FirstPlayerName = this.Player1Name.Text, 
                                        m_SecondPlayerName = this.Player2Name.Text,
                                        m_IsSecondPlayerPC = this.Player2Name.Text == this.r_Player2DefaultName 
                                    };

            if(this.getBoardSize() != -1)
            {
                this.m_IsInistializedProperly = true;
            }

            return settings;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.initControls();
        }

        private int getNextVerticalHeight(int i_ComponnentWidth)
        {
            int res = this.CurrentVerticalMargin;
            this.CurrentVerticalMargin += i_ComponnentWidth + this.r_VerticalMargin;

            return res;
        }

        private void initControls()
        {
            this.initBoardSizeSection();
            this.initPlayersSection();
            
            this.ButtonFinish.Text = "Done";
            int xPos = this.Player2Name.Left + this.Player2Name.Size.Width - this.ButtonFinish.Size.Width;
            int yPos = this.CheckBoxPlayer2.Top + this.CheckBoxPlayer2.Size.Height + this.r_VerticalMargin;
            this.ButtonFinish.Location = new Point(xPos, yPos);
            this.ButtonFinish.Click += new EventHandler(this.buttonFinish_Click);

            this.Controls.AddRange(new Control[] {ButtonFinish});

        }

        private void checkboxPlayer2_Click(object i_Sender, EventArgs i_Args)
        {
            if(this.Player2Name.Enabled == true)
            {
                    this.Player2Name.Enabled = false;
                    this.Player2Name.Text = this.r_Player2DefaultName;
            }
            else
            {
                this.Player2Name.Enabled = true;
                this.Player2Name.Text = string.Empty;
            }
        }

        private int getBoardSize()
        {
            int res = -1;

            foreach(RadioButton radioButton in this.RadioButtonsBoardSize.Values)
            {
                if(radioButton.Checked)
                {
                    res = int.Parse(radioButton.Text.Split('X')[0]);
                    break;
                }    
            }

            return res;
        }

        private void buttonFinish_Click(object i_Sender, EventArgs i_Args)
        {

            if((this.getBoardSize() != -1) && this.Player1Name.Text != String.Empty
                                           && this.Player2Name.Text != String.Empty)
            {
                this.Close();
            }
        }

        private void initPlayersSection()
        {
            this.LabelPlayers.Text = "Players:";
            this.LabelPlayers.Location = new Point(10, this.getNextVerticalHeight(this.LabelPlayers.Size.Height));
            this.LabelPlayer1.Text = "Player 1:";
            this.LabelPlayer1.Location = new Point(this.r_HorizontalMargin, this.getNextVerticalHeight(this.LabelPlayer1.Size.Height));
            this.Player1Name.Location = new Point(100, this.LabelPlayer1.Location.Y - 3);
            
            this.CheckBoxPlayer2.Text = "Player 2:";
            this.CheckBoxPlayer2.Location = new Point(this.r_HorizontalMargin, this.getNextVerticalHeight(this.CheckBoxPlayer2.Size.Height));
            this.CheckBoxPlayer2.Click += new EventHandler(this.checkboxPlayer2_Click);

            this.Player2Name.Location = new Point(100, this.CheckBoxPlayer2.Location.Y + 3);
            this.Player2Name.Enabled = false;
            this.Player2Name.Text = this.r_Player2DefaultName;

            this.Controls.AddRange(new Control[] {this.Player2Name, this.Player1Name ,this.LabelPlayers, this.LabelPlayer1, this.CheckBoxPlayer2 });
        }

        private void initBoardSizeSection()
        {
            this.LabelBoardSize.Text = "Board Size:";
            this.LabelBoardSize.Location = new Point(10, getNextVerticalHeight(this.LabelBoardSize.Size.Height));
            this.Controls.Add(this.LabelBoardSize);

            int radioButtonsXPos = this.getNextVerticalHeight(new RadioButton().Size.Height);
            for (int i = 0; i < 3; i++)
            {
                RadioButton radioButton = new RadioButton();
                String label = $"{(i + 3) * 2}X{(i + 3) * 2}";
                radioButton.Text = label;
                radioButton.Size = new Size(55, 30);
                radioButton.Location = new Point(this.r_HorizontalMargin + (i * radioButton.Size.Width), radioButtonsXPos);
                RadioButtonsBoardSize.Add(label, radioButton);
                this.Controls.Add(radioButton);
            }
        }
    }
}

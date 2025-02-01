using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace DamkaGUI
{
    internal class FormSettings : Form
    {
        private readonly int r_VeticalMergin = 25;
        private readonly int r_HorizontalMargin = 25;
        private Label LabelBoardSize { get; set; } = new Label();
        private Dictionary<string, RadioButton> RadioButtonsBoardSize { get; set; } = new Dictionary<string, RadioButton>();
        private Label LabelPlayers { get; set; } = new Label();
        private Label LabelPlayer1 { get; set; } = new Label();
        private CheckBox CheckBoxPlayer2 { get; set; } = new CheckBox();
        private TextBox Player1Name {get; set; } = new TextBox();
        private TextBox Player2Name {get; set; } = new TextBox();

        //TextBox m_TextboxPassword = new TextBox();
        //Label LabelPlayers = new Label();
        //Label m_LabelPlayer1 = new Label();
        //Label m_LabelPlayer2 = new Label();
        //Button m_ButtonOK = new Button();
        //Button m_ButtonCancel = new Button();

        public FormSettings()
        {
            this.Size = new Size(300, 300);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "GameSettings";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.initControls();
        }

        private void initControls()
        {
            

            this.initBoardSizeSection();
            this.initPlayersSection();

            this.Controls.AddRange(new Control[] { LabelBoardSize});

        }

        private void initPlayersSection()
        {
            int baseVerticalHight = 70;

            this.LabelPlayers.Text = "Players:";
            this.LabelPlayers.Location = new Point(10, baseVerticalHight);
            this.LabelPlayer1.Text = "Player 1:";
            this.LabelPlayer1.Location = new Point(this.r_HorizontalMargin, baseVerticalHight + this.r_VeticalMergin);
            this.Player1Name.Location = new Point(100, this.LabelPlayer1.Location.Y - 3);
            this.CheckBoxPlayer2.Text = "Player 2:";
            this.CheckBoxPlayer2.Location = new Point(this.r_HorizontalMargin, baseVerticalHight + this.r_VeticalMergin*2);
            this.Player2Name.Location = new Point(100, this.CheckBoxPlayer2.Location.Y + 3);
            this.Player2Name.Enabled = false;
            this.Player2Name.Text = "[Computer]";

            this.Controls.AddRange(new Control[] {this.Player2Name, this.Player1Name ,this.LabelPlayers, this.LabelPlayer1, this.CheckBoxPlayer2 });
        }

        private void initBoardSizeSection()
        {
            this.LabelBoardSize.Text = "Board Size:";
            this.LabelBoardSize.Location = new Point(10, 20);

            for (int i = 0; i < 3; i++)
            {
                RadioButton radioButton = new RadioButton();
                String label = $"{(i + 3) * 2}X{(i + 3) * 2}";
                radioButton.Text = label;
                radioButton.Size = new Size(55, 30);
                radioButton.Location = new Point(this.r_HorizontalMargin + (i * radioButton.Size.Width), 40);
                RadioButtonsBoardSize.Add(label, radioButton);
                this.Controls.Add(radioButton);
            }
        }
    }
}

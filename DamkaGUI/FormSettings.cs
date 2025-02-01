using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace DamkaGUI
{
    internal class FormSettings : Form
    {
        Label m_LabelBoardSize = new Label();
        private Dictionary<string, RadioButton> m_RadioButtonsBoardSize = new Dictionary<string, RadioButton>();
        //TextBox m_TextboxPassword = new TextBox();
        //Label m_LabelPlayers = new Label();
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
            this.m_LabelBoardSize.Text = "Board Size:";
            this.m_LabelBoardSize.Location = new Point(10, 20);

            this.initRadioButtons();

            this.Controls.AddRange(new Control[] { m_LabelBoardSize});

        }

        private void initRadioButtons()
        {
            for(int i = 0; i < 3; i++)
            {
                RadioButton radioButton = new RadioButton();
                String label = $"{(i + 3) * 2}X{(i + 3) * 2}";
                radioButton.Text = label;
                radioButton.Size = new Size(55, 30);
                radioButton.Location = new Point(10 + (i * radioButton.Size.Width), 40);
                m_RadioButtonsBoardSize.Add(label, radioButton);
                this.Controls.Add(radioButton);
            }
        }
    }
}

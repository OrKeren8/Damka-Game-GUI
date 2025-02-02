using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BackEnd;

namespace DamkaGUI
{
    internal class FormDamka : Form
    {
        private FormSettings.Settings Settings { get; set; }
        private GameManager DamkaManager { get; set; }

        public void AddSettings(FormSettings.Settings i_Settings)
        {
            this.Settings = i_Settings;
            this.initDamkaManager();
        }

        private void initDamkaManager()
        {
            Player player1 = new Player(this.Settings.firstPlayerName, i_IsPc: false, ePlayerType.White);
            Player player2 = new Player(this.Settings.secondPlayerName, this.Settings.isSecondPlayerPC, ePlayerType.Black);
            this.DamkaManager = new GameManager(player1, player2, this.Settings.boardSize);
        }

    }
}
